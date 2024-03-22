using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Support.Types;
using Events;
using Events.Args;
using Messages.Handling;
using Messages.Handling.Args;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public static class CallBackReciever
{
    private static TelegramUser? _user;
    private static Message? _message;
    public static void Init()
    {
        PriorityEventHandler.Subscribe<CallBackRecievedArgs>(OnCallBack, 1);
    }

    private static void OnClientCallBack(Client client, CallBackPacket packet)
    {
        IProcess process = new UnknownProcess();

        if (_message != null)
        {
            MessageDeleter.DeleteMessage(_message);
        }

        switch (packet.Code)
        {
            case CallBackCode.Ingnore: return;
            default: return;

            case CallBackCode.NonTypeProblem:
                process = new CreateHumanRequestProcess(client);
                break;

            case CallBackCode.GetProblemType: new GetProblemDescriptionProcess(client, packet.SendData).Start(); break;
            case CallBackCode.ItHelped: new CreateBotRequestProcess(client, packet.SendData).Start(); break;
            case CallBackCode.PrintProblemTypes: new PrintProblemsProcess(client).Start(); break;
            case CallBackCode.ClientMainMenu: new PrintMainMenuDialog(client).Start(); break;
        }

        ProcessHandler.Run(client.Id, process);
    }



    private static void OnCallBack(CallBackRecievedArgs args)
    {
        if (args.CallBack.Data == null)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(new Exception("call back data exception")));
            return;
        }

        if (args.CallBack.Message == null)
        {
            return;
        }

        _message = args.CallBack.Message;

        CallBackPacket packet = new(args.CallBack.Data);
        packet.Unpack();

        _user = DataBase.Users.Find(i => i.Id == packet.ChatId);

        if (_user is null)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(new Exception("user not found exception")));
            return;
        }

        if (_user is Client client)
        {
            OnClientCallBack(client, packet);
        }

    }
}
