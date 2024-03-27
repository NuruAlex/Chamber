using Newtonsoft.Json;

namespace Chamber.Core.Users;

[Serializable]
public abstract class TelegramUser(long chatId, string phone) : BaseEntity(chatId)
{
    public string Phone { get; set; } = phone;

    public override string ToText()
    {
        return $"UserId: {Id}, Phone: {Phone}";
    }
}
