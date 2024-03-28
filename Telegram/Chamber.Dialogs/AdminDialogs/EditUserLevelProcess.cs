using Chamber.Collections;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Support.Types;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.AdminDialogs;

internal static class EditUserLevelProcess
{
    public static async void AddUserLevel(TelegramUser admin, string userId, UserLevel newLevel)
    {
        if (!long.TryParse(userId, out var id))
        {
            return;
        }

        TelegramUser? user = DataBase.Users.Find(i => i.Id == id);

        if (user == null)
        {
            return;
        }

        if (user.AvailableLevels.Contains(newLevel))
        {
            await Sender.SendMessage(new TextMessage(admin.Id, "У этого пользователя уже есть этот уровень доступа"));
            ProcessHandler.Run(admin.Id, new PrintAdminMainMenu(admin));
        }
        else
        {
            user.AvailableLevels.Add(newLevel);
            DataBase.Users.Refresh();
            await Sender.SendMessage(new TextMessage(admin.Id, "Все прошло гладко"));
            ProcessHandler.Run(admin.Id, new PrintAdminMainMenu(admin));
        }

    }
}
