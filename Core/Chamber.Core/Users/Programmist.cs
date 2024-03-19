namespace Chamber.Core.Users;

[Serializable]
public class Programmist(long chatId, string phone) : TelegramUser(chatId, phone)
{
    public readonly int Level = 2;
}
