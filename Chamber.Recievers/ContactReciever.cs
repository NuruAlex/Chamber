using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Recievers.Args;
using Events;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public class ContactReciever
{
    public ContactReciever()
    {
        PriorityEventHandler.Subscribe<ContactRecievedArgs>(ContacRecieved, 1);
    }

    private async void ContacRecieved(ContactRecievedArgs args)
    {
        long chat = args.ChatId;
        Contact contact = args.Contact;
        DataBase.Users.Add(new Client(chat, contact.PhoneNumber, contact.FirstName));
        await Sender.SendMessage(new TextMessage(args.ChatId, $"{contact.FirstName}, вы были успешно зарегестрированы"));
    }
}
