using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Chamber.Support.Types;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintRequestDialog(TelegramUser client, string id) : IProcess
{
    public TelegramUser Client { get; set; } = client;
    private string Id { get; set; } = id;

    public async void Start()
    {
        if (Client.CurrentLevel != Core.Enums.UserLevel.Client)
        {
            ProcessHandler.TerminateProcess(Client.Id);
            return;
        }

        Request? request = DataBase.Requests.Find(i => i.Id == long.Parse(Id));

        if (request == null)
        {
            return;
        }

        string message = request.ToText();

        await Sender.SendMessage(new TextMessage(Client.Id, message)
        {
            Markup = new InlineMarkup(
                new InlineButton("Назад",
                    new CallBackPacket(Client.Id, CallBackCode.MyRequests)))
        });
    }
}
