using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Log;
using Chamber.Support.Types;
using Events;
using Events.Args;
using Messages.Building;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class CreateHumanRequestProcess(Client client) : IMultiActProcess
{
    public Client Client { get; set; } = client;
    public int Iteration { get; set; } = -1;

    public List<int> Messages = [];

    public string? Description;
    public string? FilePath;

    public async void NextAction(Message message)
    {
        Iteration++;
        Messages.Add(message.MessageId);

        if (Iteration == 0)
        {
            if (message.Text == null)
            {
                Messages.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите текст (описание проблемы):")));

                Iteration--;
                return;
            }

            Description = message.Text;

            Messages.Add(await Sender
                .SendMessage(new TextMessage(Client.Id, "Отправьте скриншот проблемы или нажмите команду пропустить:")
                {
                    Markup = new ReplyMarkup().AddButton(new ReplyButton("пропустить"))
                }));
        }

        if (Iteration == 1)
        {

            if (message.Text?.ToLower() == "пропустить")
            {
            }
            else if (message.Photo != null)
            {
                if (await Builder.BuildLocalMessage(Client.Id, message) is PhotoMessage photo)
                {
                    FilePath = photo.Media?.Path;
                    Logger.LogMessage($"{photo.Media?.Path}");
                }
            }
            else
            {
                Messages.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, отправьте скриншот или нажмите команду /пропустить:")));
                Iteration--;
                return;
            }

            int requestId = DataBase.Requests.Count + 1;

            HumanRequest request = new(requestId, Client)
            {
                Description = Description,
                FilePath = FilePath,
                Level = 1,
            };

            DataBase.Requests.Add(request);

            Messages.Add(await Sender
                   .SendMessage(new TextMessage(Client.Id, $"Ваше обращение зарегестрировано\nНомер обращения: {requestId} ")
                   {
                       Markup = new RemoveMarkup()
                   }));
            ProcessHandler.Run(Client.Id, new PrintMainMenuDialog(Client));
        }

    }

    public async void Start()
    {
        try
        {
            Messages.Add(await Sender
                .SendMessage(new TextMessage(Client.Id, "Введите описание проблемы:")));
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
        }
    }
}
