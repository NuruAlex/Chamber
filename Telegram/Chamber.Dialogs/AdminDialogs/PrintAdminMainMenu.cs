using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;
using Chamber.Core.Enums;

namespace Chamber.Dialogs.AdminDialogs;

public class PrintAdminMainMenu(TelegramUser admin) : IProcess
{
    public TelegramUser Admin { get; set; } = admin;

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Admin.Id, "Пользователи в системе")
        {
            Markup = new InlineMarkup()
            .AddButton($"Клиенты ({DataBase.Users.FindAll(i => i.AvailableLevels.Contains(UserLevel.Client)).Count})", new CallBackPacket(Admin.Id, CallBackCode.PrintClients))
            .AddRow()
            .AddButton($"Специалисты ({DataBase.Users.FindAll(i => i.AvailableLevels.Contains(UserLevel.Specialist)).Count})", new CallBackPacket(Admin.Id, CallBackCode.PrintSpesialists))
            .AddRow()
            .AddButton($"Программисты ({DataBase.Users.FindAll(i => i.AvailableLevels.Contains(UserLevel.Programmist)).Count})", new CallBackPacket(Admin.Id, CallBackCode.PrintProgrammists))
            .AddRow()
            .AddButton($"Администраторы ({DataBase.Users.FindAll(i => i.AvailableLevels.Contains(UserLevel.Admin)).Count})", new CallBackPacket(Admin.Id, CallBackCode.PrintAdmins))
        });
    }
}
