using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Processes.ValidationProcesses;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Processes.FieldRequestProcesses;

[Serializable]
public class RequireOldCertificateNumber(Client client) : IRequireDataProcess
{
    public Client Client { get; set; } = client;
    public string? OldCertificateeNumber { get; set; }
    public bool WasDone { get; set; }

    public async void NextAction(Message message)
    {
        if (!new ValidateCertificateProcess().IsValid(message))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите старый номер сертификата"));
            return;
        }


        OldCertificateeNumber = message.Text;
        WasDone = true;
    }

    public BotRequest SetSpecificValue(BotRequest request)
    {
        request.OldCertificate = OldCertificateeNumber;
        return request.Copy();
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите старый номер сертификата"));
    }
}
