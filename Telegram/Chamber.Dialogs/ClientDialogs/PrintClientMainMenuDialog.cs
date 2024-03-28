using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Processes.ClientDialogs;

[Serializable]
public class PrintClientMainMenuDialog(TelegramUser user) : IProcess
{
    public TelegramUser User { get; set; } = user;

    public async void Start()
    {
        if (User.CurrentLevel != UserLevel.Client)
        {
            return;
        }

        int requestsCount = DataBase.Requests.Count;

        await Sender.SendMessage(new TextMessage(User.Id,
            "Вас приветсвует поддержка Торгово-промышленной палаты РФ, здесь вы можете найти решение проблем")
        {
            Markup = new InlineMarkup(
                new InlineButton("Создать обращение", new CallBackPacket(User.Id, CallBackCode.PrintProblemTypes)),
                new InlineButton($"Мои обращения ({requestsCount})", new CallBackPacket(User.Id, CallBackCode.MyRequests))
                )
        });
    }
}
