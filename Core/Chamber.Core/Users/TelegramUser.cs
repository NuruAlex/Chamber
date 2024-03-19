namespace Chamber.Core.Users;

[Serializable]
public abstract class TelegramUser(long chatId, string phone) : BaseEntity(chatId)
{
    public readonly string Phone = phone;
}
