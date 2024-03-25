using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs;
using Chamber.Log;
using Chamber.Recievers.Archieves;
using Chamber.Support.Types;
using Events;
using Events.Args;
using Messages.Handling;
using Messages.Handling.Args;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public static class CallBackReciever
{
    private static Message? _message;

    public static void Init()
    {
        PriorityEventHandler.Subscribe<CallBackRecievedArgs>(OnCallBack, 1);
    }

    private static void OnClientCallBack(Client client, CallBackPacket packet)
    {
        IProcess process = CallBackDialogArchieve.GetClientProcess(client, packet);

        if (_message != null)
        {
            MessageDeleter.DeleteMessage(_message);
        }
        ProcessHandler.Run(client.Id, process);
        Logger.LogMessage($"CallBackReciever / OnClientCallBack / packet code {packet.Code} / {process.GetType().Name}");
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
            PriorityEventHandler.Invoke(new ErrorArgs(new Exception("call back message exception")));
            return;
        }

        _message = args.CallBack.Message;

        CallBackPacket packet = new(args.CallBack.Data);
        packet.Unpack();

        TelegramUser? user = DataBase.Users.Find(i => i.Id == packet.ChatId);

        if (user is null)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(new Exception("user not found exception")));
            return;
        }

        if (user is Client client)
        {
            OnClientCallBack(client, packet);
        }

    }
}
