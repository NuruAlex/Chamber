using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public abstract class Request(long id) : BaseEntity(id)
{
    public Client Client { get; set; }
}
