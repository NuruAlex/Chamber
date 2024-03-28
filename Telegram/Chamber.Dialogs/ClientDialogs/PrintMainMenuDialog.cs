using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Processes.ClientDialogs;

[Serializable]
public class PrintMainMenuDialog(Client client) : IProcess
{
    public Client Client { get; set; } = client;

    public async void Start()
    {
        int requestsCount = DataBase.Requests.Count;
        
        await Sender.SendMessage(new TextMessage(Client.Id,
            "Вас приветсвует поддержка Торгово-промышленной палаты РФ, здесь вы можете найти решение проблем")
        {
            Markup = new InlineMarkup(
                new InlineButton("Создать обращение", new CallBackPacket(Client.Id, CallBackCode.PrintProblemTypes)),
                new InlineButton($"Мои обращения ({requestsCount})", new CallBackPacket(Client.Id, CallBackCode.MyRequests))
                )
        });
    }
}
