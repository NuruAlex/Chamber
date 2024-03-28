using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.AdminDialogs;

[Serializable]
public class PrintClientDialog(TelegramUser admin, string clientId) : IProcess
{
    public TelegramUser Admin { get; set; } = admin;

    public string ClientId { get; set; } = clientId;
    public async void Start()
    {
        if (!long.TryParse(ClientId, out var clientId))
        {
            return;
        }

        TelegramUser? client = DataBase.Users.Find(i => i.Id == clientId);
        
        if (client == null)
        {
            return;
        }

        InlineMarkup markup = new(
          new InlineButton("Сделать администратором", new CallBackPacket(Admin.Id, CallBackCode.MakeNewAdmin, sendData: client.Id.ToString())),
          new InlineRow(),
          new InlineButton("Назначить специалистом", new CallBackPacket(Admin.Id, CallBackCode.MakeNewSpecialist, sendData: client.Id.ToString())),
          new InlineRow(),
          new InlineButton("Назначить программистом", new CallBackPacket(Admin.Id, CallBackCode.MakeNewProgrammist, sendData: client.Id.ToString())));

        await Sender.SendMessage(new TextMessage(Admin.Id, $"Клиент: {client.Id}", markup));
    }
}
