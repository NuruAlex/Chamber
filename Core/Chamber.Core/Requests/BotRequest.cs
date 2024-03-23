using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class BotRequest(long id, Client client, string problemType) : Request(id, client)
{
    public string ProblemType { get; set; } = problemType;

}
