using Chamber.Recievers.Args;
using Events;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Recievers;

public class TextReciever
{
    public TextReciever()
    {
        PriorityEventHandler.Subscribe<TextRecievedArgs>(TextRecieved, 1);
    }

    private async void TextRecieved(TextRecievedArgs args)
    {
        string text = args.Text;

        if (text == "/start")
        {
            await Sender.SendMessage(new TextMessage(args.ChatId, 
                "Добрый день, отправьте пожалйста контакты")
            {
                Markup = new RequestContactMarkup("Отправить")
            });

            return;
        }
    }
}
