using Newtonsoft.Json;

namespace Chamber.Processes;

[JsonObject]
public interface IProcess
{
    public void Start();
}
