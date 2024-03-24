using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Messages.Building.InnerBuilders;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class CreateHumanRequestProcess(Client client) : IClientMultiActProcess
{
    public Client Client { get; set; } = client;
    public int Iteration { get; set; } = -1;

    private readonly List<int> _ids = [];

    private readonly string? _name;
    private string? _description;

    public async void NextAction(Message message)
    {
        Iteration++;
        _ids.Add(message.MessageId);


        if (Iteration == 0)
        {
            if (message.Text == null)
            {
                _ids.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите текст (описание проблемы):")));

                Iteration--;
                return;
            }

            _description = message.Text;

            _ids.Add(await Sender
                .SendMessage(new TextMessage(Client.Id, "Отправьте скриншот проблемы или нажмите команду /пропустить:")));
        }

        if (Iteration == 1)
        {
            int requestId = DataBase.Requests.Count + 1;
            HumanRequest request = new(requestId, Client)
            {
                Name = _name,
                Description = _description,
                Level = 1,
            };

            if (message.Text?.ToLower() == "/пропустить")
            {
                DataBase.Requests.Add(request);

                _ids.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, $"Ваше обращение зарегестрировано\nНомер обращения: {requestId} ")));
            }
            else if (message.Photo != null)
            {
                PhotoBuilder photoBuilder = new();

                var m = photoBuilder.BuildWithUriOrFileId(Client.Id, message.Photo[message.Photo.Length - 1].FileId);

                if (m is PhotoMessage photo)
                {
                    request.FilePath = photo.Media?.UriOrFileId;
                }

                DataBase.Requests.Add(request);

                _ids.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, $"Ваше обращение зарегестрировано\nНомер обращения: {requestId} ")));
            }
            else
            {
                _ids.Add(await Sender
                    .SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, отправьте скриншот или нажмите команду /пропустить:")));
                Iteration--;
                return;
            }
        }

    }

    public async void Start()
    {
        _ids.Add(await Sender
            .SendMessage(new TextMessage(Client.Id, "Введите описание проблемы:")));
    }
}
