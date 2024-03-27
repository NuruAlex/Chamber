using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Processes.ValidationProcesses;
using Messages.Core.Types;
using Messages.Senders;
using Newtonsoft.Json;
using Telegram.Bot.Types;


namespace Chamber.Processes.FieldRequestProcesses;

[Serializable]
public class RequireRequestNumberProcess(Client client) : IRequireDataProcess
{
    [JsonProperty]
    public string? RequestNumber { get; set; }

    [JsonProperty]
    public bool WasDone { get; set; } = false;

    public Client Client { get; set; } = client;

    public async void NextAction(Message message)
    {
        if (!new ValidateRequestId().IsValid(message))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, ожидается номер заявки:"));
            return;
        }

        RequestNumber = message.Text;
        WasDone = true;
    }

    public BotRequest SetSpecificValue(BotRequest request)
    {
        request.RequestId = RequestNumber;
        return request.Copy();
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите номер заявки:"));
    }
}
