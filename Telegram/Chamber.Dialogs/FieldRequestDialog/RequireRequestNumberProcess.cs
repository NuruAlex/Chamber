using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[Serializable]
public class RequireRequestNumberProcess(Client client) : IDataProcess
{
    public long RequestNumber { get; set; }
    public bool WasDone { get; set; } = false;

    public Client Client { get; set; } = client;
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
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите номер заявки"));
        WasDone = false;
    }
}
