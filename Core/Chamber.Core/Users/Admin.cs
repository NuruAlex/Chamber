namespace Chamber.Core.Users;

[Serializable]
public class Admin(long chatId, string phone) : TelegramUser(chatId, phone)
{
}
