using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Messages.Core.Reply.Markups;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.AdminDialogs;

[Serializable]
public class PrintClientsList(TelegramUser admin) : IProcess
{
    public TelegramUser Admin { get; set; } = admin;

    public async void Start()
    {
        List<TelegramUser> users = DataBase.Users.FindAll(i => i.AvailableLevels.Contains(UserLevel.Client));
        InlineMarkup markup = new();


        markup.AddButton("Назад", new CallBackPacket(Admin.Id, CallBackCode.MainMenu)).AddRow();

        string text = "";

        if (users.Count == 0)
        {
            text = "Клиентов не найдено";
        }
        else
        {
            text = $"Клиенты ({users.Count})";
        }

        foreach (TelegramUser user in users)
        {
            markup.AddButton($" {user.Phone} ({user.FirstName})", new CallBackPacket(Admin.Id, CallBackCode.PrintClient, sendData: user.Id.ToString()))
                .AddRow();
        }

        await Sender.SendMessage(new TextMessage(Admin.Id, text, markup));
    }
}
