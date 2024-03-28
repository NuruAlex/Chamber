using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class HumanRequest(long id, Client client, string name) : Request(id, client, name)
{
    public string? Description { get; set; }
    public string? FilePath { get; set; }

    public override string ToText()
    {
        return $"Описание проблемы: {Description}";
    }
}
