namespace Chamber.Processes;

[Serializable]
public class UnknownProcess : IProcess
{
    public void Start()
    {
        throw new NotImplementedException();
    }
}
