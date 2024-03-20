namespace Chamber.Serialization.Args;

public class SerializationError(string path, string method, string type)
{
    public readonly string Path = path;
    public readonly string TypeName = type;
    public readonly string Method = method;
}
