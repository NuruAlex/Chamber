using Chamber.Core.Requests;
using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;
using Chamber.Processes.ValidationProcesses;

namespace Chamber.Processes.FieldRequestProcesses;

[Serializable]
public class RequireNewBlankNumberProcess(Client client) : IRequireDataProcess
{
    public Client Client { get; set; } = client;
    public bool WasDone { get; set; }
    public string? NewBlankNumber { get; set; }

    public async void NextAction(Message message)
    {
        if (!new ValidateBlankProcess().IsValid(message))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите новый номер бланка"));
            return;
        }
       

        NewBlankNumber = message.Text;
        WasDone = true;
    }

    public BotRequest SetSpecificValue(BotRequest request)
    {
        request.NewBlank = NewBlankNumber;
        return request.Copy();
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите новый номер бланка"));
        WasDone = false;
    }
}
