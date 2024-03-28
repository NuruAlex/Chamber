using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public abstract class Request(long id, Client client, string name) : BaseEntity(id)
{
    public string Name { get; set; } = name;
    public Client Client { get; set; } = client;
    public TelegramUser? Executor { get; set; }
    public int Level { get; set; }
    public DateTime? DoneTime { get; set; }
    public string? Status { get; set; }
}
