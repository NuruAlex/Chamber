using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Recievers.Args;
using Events;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public static class ContactReciever
{
    public static void Init()
    {
        PriorityEventHandler.Subscribe<ContactRecievedArgs>(ContacRecieved, 1);
    }

    private static async void ContacRecieved(ContactRecievedArgs args)
    {
        long chat = args.ChatId;

        Contact contact = args.Contact;

        Client client = new(chat, contact.PhoneNumber, contact.FirstName);

        DataBase.Users.Add(client);

        int id = await Sender.SendMessage(new TextMessage(args.ChatId, $"{contact.FirstName}, вы были успешно зарегестрированы")
        {
            Markup = new RemoveMarkup()
        });

        Thread.Sleep(1000);
        MessageDeleter.DeleteMessage(chat, id);
        new PrintMainMenuDialog(client).Start();
    }
}
