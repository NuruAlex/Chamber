using Chamber.Core.Requests;
using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[Serializable]
public class RequeireOldCertificateNumber(Client client) : IRequireDataProcess
{
    public Client Client { get; set; } = client;
    public long OldCertificateeNumber { get; set; }
    public bool WasDone { get; set; }

    public async void NextAction(Message message)
    {
        if (message.Text == null)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите старый номер сертификата"));
            return;
        }
        if (!long.TryParse(message.Text, out var number))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, номер сертификата должен быть числом:"));
            return;
        }

        OldCertificateeNumber = number;
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
        WasDone = false;
    }
}
