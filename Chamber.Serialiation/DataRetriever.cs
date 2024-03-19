using Chamber.Serialiation.InnerRetirver;

namespace Chamber.Serialiation;

public class DataRetriever
{
    private readonly object _lock = new();
    private IRetriever _retriever = new UnknownRetriever();

    public DataRetriever()
    {
        Retriever = new JsonRetriever();
    }

    public IRetriever Retriever
    {
        get => _retriever;
        set
        {
            if (value == null)
                _retriever = new UnknownRetriever();
            else
                _retriever = value;
        }
    }


    public List<T> LoadFromFile<T>() => LoadFromFile<T>(FileArchieve.PathByType<T>() + _retriever.Extension) ?? new();
    public void SaveToFile<T>(List<T> data) => SaveToFile(data, FileArchieve.PathByType<T>() + _retriever.Extension);

    public List<T> LoadFromFile<T>(string? path)
    {
        if (path == null)
        {
            throw new Exception($"Unknown path inner retriever  {_retriever?.GetType().Name} /T:  {typeof(T).Name} ");
        }

        lock (_lock)
        {
            try
            {
                return _retriever?.LoadFromFile<T>(path ?? "") ?? [];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" DataBase / LoadFromFile<T>() / Type: {_retriever?.GetType().Name} / T:  {typeof(T).Name} "));
            }
        }
    }

    public void SaveToFile<T>(List<T> data, string? path)
    {
        if (path == null)
        {
            throw new Exception($"Unknown path inner retriever  {_retriever?.GetType().Name} /T:  {typeof(T).Name} "));
        }

        lock (_lock)
        {
            try
            {
                _retriever?.SaveToFile(data, path ?? "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $"DataBase / Save<T>() / Type: {_retriever?.GetType().Name} / T:  {typeof(T).Name} "));
            }
        }
    }

}
