using Newtonsoft.Json;

namespace Chamber.Core.Users;

[JsonObject]
public abstract class TelegramUser(long chatId, string phone) : BaseEntity(chatId)
{
    [JsonProperty(nameof(Phone))]
    public string Phone { get; set; } = phone;
}
