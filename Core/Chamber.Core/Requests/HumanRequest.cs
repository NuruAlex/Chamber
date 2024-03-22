using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class HumanRequest(long id, Client client) : Request(id, client)
{
    public DateTime? DoneTime { get; set; }
    public int Level { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    public TelegramUser? Executor { get; set; }
}
