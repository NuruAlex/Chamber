using Newtonsoft.Json;

namespace Chamber.Core;

[JsonObject]
public abstract class BaseEntity(long id)
{
    public long Id { get; set; } = id;
    public DateTime Creation { get; set; } = DateTime.Now;

    public abstract string ToText();
}
