namespace Chamber.Core;

[Serializable]
public class BaseEntity
{
    public readonly DateTime Creation;

    public BaseEntity()
    {
        Creation = DateTime.Now;
    }
}
