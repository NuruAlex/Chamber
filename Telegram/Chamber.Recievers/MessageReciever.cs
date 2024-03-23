using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Recievers.Args;
using Chamber.Support.Types;
using Events;
using Events.Args;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Handling.Args;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Recievers;

public static class MessageReciever
{
    private static Message? _message;
    private static TelegramUser? _user;

    public static void Init()
    {
        PriorityEventHandler.Subscribe<MessageRecievedArgs>(OnMessage, 2);
        PriorityEventHandler.Subscribe<TextRecievedArgs>(OnText, 1);
        PriorityEventHandler.Subscribe<ContactRecievedArgs>(OnContacts, 1);

    }

    private static async void OnContacts(ContactRecievedArgs args)
    {
        long chat = args.ChatId;

        Contact contact = args.Contact;

        if (_user == null)
        {
            Client client = new(chat, contact.PhoneNumber, contact.FirstName);

            DataBase.Users.Add(client);

            int id = await Sender.SendMessage(new TextMessage(args.ChatId, $"{contact.FirstName}, вы были успешно зарегестрированы")
            {
                Markup = new RemoveMarkup()
            });

          /*  Thread.Sleep(1000);

            MessageDeleter.DeleteMessage(chat, id);*/

            new PrintMainMenuDialog(client).Start();
        }
    }

    private static void ClientWrited(Client client, Message message)
    {
        if (ProcessHandler.NextAction(client.Id, message))
        {
            PriorityEventHandler.Invoke(new LogMessage("Вызвался метод ClientWrited / MessageReciever"));
        }
    }

    private static void OnMessage(MessageRecievedArgs args)
    {
        _message = args.Message;

        _user = DataBase.Users.Find(i => i.Id == _message.Chat.Id);

        if (_user != null)
        {
            if (_user is Client client)
            {
                ClientWrited(client, _message);
            }
        }

        if (_message.Contact != null)
        {
            PriorityEventHandler.Invoke(new ContactRecievedArgs(_message.Chat.Id, _message.Contact));
        }

        if (_message.Text != null)
        {
            PriorityEventHandler.Invoke(new TextRecievedArgs(_message.Chat.Id, _message.Text));
        }
    }

    private static async void OnText(TextRecievedArgs args)
    {
        string text = args.Text;
        long chat = args.ChatId;

        if (text == "/start")
        {
            if (_user != null && _user is Client client)
            {
                int id = await Sender.SendMessage(new TextMessage(client.Id, "Вы уже зарегистрированы"));
                Thread.Sleep(1000);
                MessageDeleter.DeleteMessage(chat, id);
            }

            await Sender.SendMessage(new TextMessage(args.ChatId, "Добрый день, отправьте пожалйста контакты")
            {
                Markup = new RequestContactMarkup("Отправить")
            });

            return;
        }
    }
}
