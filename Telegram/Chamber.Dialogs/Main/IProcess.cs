using Newtonsoft.Json;

namespace Chamber.Dialogs.Main;

[JsonObject]
public interface IProcess
{
    public void Start();
}
