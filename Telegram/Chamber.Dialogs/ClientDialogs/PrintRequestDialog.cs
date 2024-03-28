using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintRequestDialog(Client client, string id) : IProcess
{
    public Client Client { get; set; } = client;
    private string Id { get; set; } = id;

    public async void Start()
    {
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
