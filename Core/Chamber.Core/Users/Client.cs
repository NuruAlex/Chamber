namespace Chamber.Core.Users;

[Serializable]
public class Client(long chatId, string phone) : TelegramUser(chatId, phone)
{
}
