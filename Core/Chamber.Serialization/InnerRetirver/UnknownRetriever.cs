namespace Chamber.Serialization.InnerRetirver;

public class UnknownRetriever : IRetriever
{
    public string Extension => throw new NotImplementedException();

    public List<T> LoadFromFile<T>(string path)
    {
        throw new NotImplementedException();
    }

    public void SaveToFile<T>(List<T> items, string path)
    {
        throw new NotImplementedException();
    }
}