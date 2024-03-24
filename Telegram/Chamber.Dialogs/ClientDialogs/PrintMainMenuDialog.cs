using Chamber.CallBack.Types;
using Chamber.Core.Users;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintMainMenuDialog(Client client) : IProcess
{
    public Client Client { get; set; } = client;

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Вас приветсвует поддержка Торгово-промышленной палаты РФ, здесь вы можете найти решение проблем")
        {
            Markup = new InlineMarkup(
                new InlineButton("Создать обращение", new CallBackPacket(Client.Id, CallBackCode.PrintProblemTypes))
                )
        });
    }
}
