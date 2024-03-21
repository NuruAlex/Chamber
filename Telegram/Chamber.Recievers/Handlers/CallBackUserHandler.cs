using Chamber.Core.Users;

namespace Chamber.Recievers.Handlers;

public static class CallBackUserHandler
{
    public static TelegramUser? User { get; set; }

    public static bool IsClient()
    {
        return User != null && User is Client;
    }

    public static bool IsSpecialist()
    {
        return User != null && User is Specialist;
    }

    public static bool IsProgrammist()
    {
        return User != null && User is Programmist;
    }
}
