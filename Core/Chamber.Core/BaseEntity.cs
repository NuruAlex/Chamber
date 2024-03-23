using Newtonsoft.Json;

namespace Chamber.Core;

[JsonObject]
public class BaseEntity(long id)
{
    [JsonProperty]
    public long Id { get; set; } = id;

    [JsonProperty]
    public DateTime Creation { get; set; } = DateTime.Now;
}
