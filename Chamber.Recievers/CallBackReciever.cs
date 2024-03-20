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

        Message m = args.CallBack.Message;

        long chat = m.Chat.Id;
        string callBack = args.CallBack.Data;

        if (callBack != "1")
        {
            
        }
    }
}
