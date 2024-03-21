namespace Chamber.Core.Users;

[Serializable]
public class Client(long chatId, string phone, string firstName) : TelegramUser(chatId, phone)
{
    public readonly string FirstName = firstName;

}
