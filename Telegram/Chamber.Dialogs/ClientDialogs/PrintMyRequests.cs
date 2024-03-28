using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Chamber.Support.Types;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintMyRequests(TelegramUser client) : IProcess
{
    public TelegramUser Client { get; set; } = client;
    public async void Start()
    {
        if (Client.CurrentLevel != Core.Enums.UserLevel.Client)
        {
            ProcessHandler.TerminateProcess(Client.Id);
            return;
        }

        string message = "Ваши обращения";
        List<Request> requests = DataBase.Requests.Items;
        InlineMarkup markup = new(
            new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.MainMenu)),
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
