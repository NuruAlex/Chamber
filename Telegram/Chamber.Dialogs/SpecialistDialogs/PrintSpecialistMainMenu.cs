using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.SpecialistDialogs;

[Serializable]
public class PrintSpecialistMainMenu(TelegramUser specialist) : IProcess
{
    public TelegramUser Specialist { get; set; } = specialist;

    public async void Start()
    {
        int requestCount = DataBase.Requests.FindAll(i => i.Level == 1).Count;

        await Sender.SendMessage(new TextMessage(Specialist.Id, "Главное меню")
        {
            Markup = new InlineMarkup().AddButton(new($"Заявки первого уровня ({requestCount})", new CallBackPacket(Specialist.Id, code: CallBackCode.PrintSpecialistRequests)))
        });
    }
}
