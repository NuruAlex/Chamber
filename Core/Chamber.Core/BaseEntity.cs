namespace Chamber.Core;

[Serializable]
public class BaseEntity(long id)
{
    public long Id = id;

    public DateTime Creation = DateTime.Now;
}
