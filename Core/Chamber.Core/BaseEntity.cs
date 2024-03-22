using Newtonsoft.Json;

namespace Chamber.Core;

[JsonObject]
public class BaseEntity(long id)
{
    [JsonProperty("Id")]
    public long Id = id;

    [JsonProperty("Creation")]
    public DateTime Creation = DateTime.Now;
}
