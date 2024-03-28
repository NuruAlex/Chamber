using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;

namespace Chamber.Dialogs.AdminDialogs;

[Serializable]
public class MakeSpecialistProcess(TelegramUser admin, string userId) : IProcess
{
    public TelegramUser Admin { get; set; } = admin;
    public string UserId { get; set; } = userId;

    public void Start()
    {
        EditUserLevelProcess.AddUserLevel(Admin, UserId, UserLevel.Specialist);
    }
}
