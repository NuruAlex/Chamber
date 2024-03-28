using Chamber.Core.Enums;

namespace Chamber.Core.Users;

[Serializable]
public class TelegramUser(long chatId, string phone, string firstName, List<UserLevel> availableLevels) : BaseEntity(chatId)
{
    public string Phone { get; set; } = phone;

    public List<UserLevel> AvailableLevels { get; set; } = availableLevels;
    public UserLevel CurrentLevel { get; set; } = availableLevels[0];
    public string FirstName { get; set; } = firstName;

    public override string ToText()
    {
        return $"UserId: {Id}, Phone: {Phone}";
    }
}
