using Chamber.Core.Requests;
using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;
using Chamber.Processes.ValidationProcesses;

namespace Chamber.Processes.FieldRequestProcesses;

[Serializable]
public class RequireOldBlankNumber(Client client) : IRequireDataProcess
{
    public string? OldBlankNumber { get; set; }
    public bool WasDone { get; set; } = false;
    public Client Client { get; set; } = client;

    public async void NextAction(Message message)
    {
        if (!new ValidateBlankProcess().IsValid(message))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите старый номер бланка:"));
            return;
        }
        

        OldBlankNumber = message.Text;
        WasDone = true;
    }

    public BotRequest SetSpecificValue(BotRequest request)
    {
        request.OldBlank = OldBlankNumber;
        return request.Copy();
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите старый номер бланка"));
    }
}
