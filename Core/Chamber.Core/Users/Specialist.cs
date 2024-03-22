﻿using Newtonsoft.Json;

namespace Chamber.Core.Users;

[JsonObject]
public class Specialist(long chatId, string phone) : TelegramUser(chatId, phone)
{
    [JsonProperty]
    public readonly int Level = 1;
}
