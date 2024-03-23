using Newtonsoft.Json;

namespace Chamber.Serialization.InnerRetirver;

public class JsonRetriever : IRetriever
{
    public string Extension => ".json";

    private readonly JsonSerializerSettings _serializeSettings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
    };

    public List<T> LoadFromFile<T>(string path)
    {
        string json = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<List<T>>(json, _serializeSettings) 
            ?? [];
    }

    public void SaveToFile<T>(List<T> data, string path)
    {
        string json = JsonConvert.SerializeObject(data, _serializeSettings);

        File.WriteAllText(path, json);
    }
}
