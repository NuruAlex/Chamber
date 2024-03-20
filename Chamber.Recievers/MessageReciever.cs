using Chamber.Recievers.Args;
using Events;
using Messages.Handling.Args;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public class MessageReciever
{
    public MessageReciever()
    {
        PriorityEventHandler.Subscribe<MessageRecievedArgs>(OnMessage, 2);
    }

    private void OnMessage(MessageRecievedArgs args)
    {
        Message message = args.Message;

        if (message.Text != null)
        {
            PriorityEventHandler.Invoke(new TextRecievedArgs(message.Chat.Id, message.Text));
        }
        if (message.Contact != null)
        {
            PriorityEventHandler.Invoke(new ContactRecievedArgs(message.Chat.Id, message.Contact));
        }
    }
}
