namespace Chamber.Core;

[Serializable]
public class BaseEntity(long id)
{
    public readonly long Id = id;

    public readonly DateTime Creation = DateTime.Now;
}
