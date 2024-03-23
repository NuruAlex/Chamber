using Newtonsoft.Json;

namespace Chamber.Core.Users;

[JsonObject]
public class Client(long chatId, string phone, string firstName) : TelegramUser(chatId, phone)
{
    [JsonProperty]
    public string FirstName { get; set; } = firstName;

}
