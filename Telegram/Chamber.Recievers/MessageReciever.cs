using Chamber.Collections;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Chamber.Processes.ClientDialogs;
using Chamber.Recievers.Archieves;
using Chamber.Recievers.Args;
using Chamber.Support.Types;
using Events;
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
            TelegramUser client = new(chat, contact.PhoneNumber, contact.FirstName, [UserLevel.Client]);

            DataBase.Users.Add(client);

            int id = await Sender.SendMessage(new TextMessage(args.ChatId, $"{contact.FirstName}, вы были успешно зарегестрированы")
            {
                Markup = new RemoveMarkup()
            });

            Thread.Sleep(2000);

            MessageDeleter.DeleteMessage(chat, id);

            new PrintClientMainMenuDialog(client).Start();
        }
    }

    private static void UserWrited(TelegramUser client, Message message)
    {
        if (ProcessHandler.NextAction(client.Id, message))
        {
        }
    }

    private static void OnMessage(MessageRecievedArgs args)
    {
        _message = args.Message;
        _user = DataBase.Users.Find(i => i.Id == _message.Chat.Id);

        if (_user != null)
        {
            UserWrited(_user, _message);
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
        string text = args.Text.ToLower();
        long chat = args.ChatId;

        if (text.StartsWith("/as") && _user != null)
        {
            UserLevel? target = UserLevelAchieve.GetUserLevel(text);

            if (target == null)
            {
                await Sender.SendMessage(new TextMessage(_user.Id, "Такого уровня доступа не существует"));
                return;
            }

            if (!_user.AvailableLevels.Contains((UserLevel)target))
            {
                await Sender.SendMessage(new TextMessage(_user.Id, "У вас нет этого уровня доступа"));
                return;
            }
            else
            {
                _user.CurrentLevel = (UserLevel)target;
                text = "/start";
            }
        }
        if (text == "/start")
        {
            if (_user != null)
            {
                int id = await Sender.SendMessage(new TextMessage(_user.Id, "Вы уже зарегистрированы")
                {
                    Markup = new RemoveMarkup()
                });

                Thread.Sleep(1000);
                MessageDeleter.DeleteMessage(chat, id);
                IProcess process = CallBackDialogArchieve.GetProcess(_user, new CallBack.Types.CallBackPacket(_user.Id, CallBack.Types.CallBackCode.MainMenu));

                ProcessHandler.Run(id, process);

                return;
            }

            await Sender.SendMessage(new TextMessage(args.ChatId, "Добрый день, отправьте пожалйста контакты")
            {
                Markup = new RequestContactMarkup("Отправить контакты")
            });

        }

    }
}
