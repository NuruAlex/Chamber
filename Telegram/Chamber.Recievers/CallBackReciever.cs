using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Recievers.CallBack;
using Events;
using Messages.Handling.Args;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public class CallBackReciever
{
    public CallBackReciever()
    {
        PriorityEventHandler.Subscribe<CallBackRecievedArgs>(OnCallBack, 1);
    }


    private async void OnCallBack(CallBackRecievedArgs args)
    {

        if (args.CallBack.Message == null)
        {
            return;
        }
        if (args.CallBack.Data == null)
        {
            return;
        }

        CallBackPacket packet = new(args.CallBack.Data);
        packet.Unpack();

        Message m = args.CallBack.Message;

        long chat = packet.ChatId;

        TelegramUser? callBackUser = DataBase.Users.Find(i => i.Id == chat);

        if (callBackUser is null)
        {
            return;
        }

        if (callBackUser is Client client)
        {
            string callBack = packet.SendData;

            if (packet.Code == CallBackCode.GetProblemType)
            {

            }
        }




    }
}
