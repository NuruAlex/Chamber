namespace Chamber.Core.Users;

[Serializable]
public class Specialist(long chatId, string phone) : TelegramUser(chatId, phone)
{
    public readonly int Level = 1;
}
