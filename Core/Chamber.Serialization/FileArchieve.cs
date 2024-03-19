namespace Chamber.Serialiation;

internal static class FileArchieve
{
    private static readonly Dictionary<Type, string> _pathByType = new()
    {
    };

    public static string PathByType<T>()
    {
        return _pathByType[typeof(T)];
    }
}