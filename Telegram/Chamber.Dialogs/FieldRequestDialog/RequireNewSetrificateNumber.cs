using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[Serializable]
public class RequireNewSetrificateNumber(Client client) : IDataProcess
{
    public long NewCertificateNumber { get; set; }
    public bool WasDone { get; set; }
    public Client Client { get; set; } = client;
 
    public async void NextAction(Message message)
    {
        if (message.Text == null)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите новый номер сертификата"));
            return;
        }
        if (!long.TryParse(message.Text, out var number))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, номер сертификата должен быть числом:"));
            return;
        }

        NewCertificateNumber = number;
        WasDone = true;
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите новый номер сертификата"));
        WasDone = false;
    }
}
