namespace Chamber.Core.Requests;

[Serializable]
public class BotRequest(long id) : Request(id)
{
    public string ProblemType { get; set; }
}
