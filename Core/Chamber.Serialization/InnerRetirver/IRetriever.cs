namespace Chamber.Serialization.InnerRetirver;

public interface IRetriever
{
    string Extension { get; }

    List<T> LoadFromFile<T>(string path);

    void SaveToFile<T>(List<T> items, string path);
}