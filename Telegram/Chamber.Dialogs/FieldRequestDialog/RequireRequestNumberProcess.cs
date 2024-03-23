using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[JsonObject]
public class RequireRequestNumberProcess : IDataProcess
{
    [JsonProperty]
    public long RequestNumber { get; set; }
    [JsonProperty]
    public bool WasDone { get; set; }

    public Client Client { get; set; }

    public RequireRequestNumberProcess(Client client)
    {
        Client = client;
        WasDone = false;
    }

    public async void NextAction(Message message)
    {
        if (message.Text == null)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, ожидается номер заявки:"));
            return;
        }
        if (!long.TryParse(message.Text, out long number))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, номер заявки должен быть числом:"));
            return;
        }

        RequestNumber = number;
        WasDone = true;
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите номер заявки:"));
    }
}
