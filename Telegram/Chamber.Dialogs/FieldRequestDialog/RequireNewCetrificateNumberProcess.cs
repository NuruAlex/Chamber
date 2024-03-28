using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Processes.ValidationProcesses;
using Chamber.Support.Types;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Processes.FieldRequestProcesses;

[Serializable]
public class RequireNewCetrificateNumberProcess(TelegramUser client) : IRequireDataProcess
{
    public string? NewCertificateNumber { get; set; }
    public bool WasDone { get; set; } = false;
    public TelegramUser Client { get; set; } = client;

    public async void NextAction(Message message)
    {
        if (!new ValidateCertificateProcess().IsValid(message))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите новый номер сертификата"));
            return;
        }

        NewCertificateNumber = message.Text;
        WasDone = true;
    }

    public BotRequest SetSpecificValue(BotRequest request)
    {
        request.NewCertificate = NewCertificateNumber;
        return request.Copy();
    }

    public async void Start()
    {
        if (Client.CurrentLevel != Core.Enums.UserLevel.Client)
        {
            ProcessHandler.TerminateProcess(Client.Id);
            return;
        }

        await Sender.SendMessage(new TextMessage(Client.Id, "Введите новый номер сертификата:"));
    }
}
