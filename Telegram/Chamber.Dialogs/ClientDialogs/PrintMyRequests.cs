using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintMyRequests(Client client) : IProcess
{
    public Client Client { get; set; } = client;
    public async void Start()
    {
        string message = "Ваши обращения";
        List<Request> requests = DataBase.Requests.Items;
        InlineMarkup markup = new(
            new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.ClientMainMenu)),
            new InlineRow());

        foreach (Request request in requests)
        {
            markup.AddButton(new
                (request.Name + " от " + request.Creation.ToString("yyyy-MM-dd hh:mm:ss"), new CallBackPacket(Client.Id, CallBackCode.GetRequest, sendData: request.Id.ToString())))
                .AddRow();
        }

        await Sender.SendMessage(new TextMessage(Client.Id, message, markup));
    }
}
