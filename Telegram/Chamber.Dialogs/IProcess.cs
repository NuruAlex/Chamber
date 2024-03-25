using Newtonsoft.Json;

namespace Chamber.Dialogs;

[JsonObject]
public interface IProcess
{
    public void Start();
}
