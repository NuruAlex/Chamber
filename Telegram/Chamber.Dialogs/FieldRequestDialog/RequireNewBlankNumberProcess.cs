using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[Serializable]
public class RequireNewBlankNumberProcess(Client client) : IDataProcess
{
    public Client Client { get; set; } = client;
    public bool WasDone { get; set; }
    public long NewBlankBumber { get; set; }

    public async void NextAction(Message message)
    {
        if (message.Text == null)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите новый номер бланка"));
            return;
        }
        if (!long.TryParse(message.Text, out var number))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, номер бланка должен быть числом:"));
            return;
        }

        NewBlankBumber = number;
        WasDone = true;
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите новый номер бланка"));
        WasDone = false;
    }
}
