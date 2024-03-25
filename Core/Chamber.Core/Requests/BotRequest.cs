using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class BotRequest(long id, Client client) : Request(id, client)
{
    public long? OldCertificate { get; set; }
    public long? NewCertificate { get; set; }
    public long? RequestId { get; set; }
    public long? NewBlank { get; set; }
    public long? OldBlank { get; set; }
}
