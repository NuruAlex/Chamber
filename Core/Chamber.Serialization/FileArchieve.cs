namespace Chamber.Serialization;

public static class FileArchieve
{
    private static readonly string _initialDirectory = "Resources\\Serialization\\";

    public static string PathByType<T>()
    {
        return _initialDirectory + typeof(T).Name;
    }
}