using Newtonsoft.Json;

namespace Chamber.Dialogs.Main;

[JsonObject]
public interface IMultiActProcess : IOneActProcess
{
    int Iteration { get; set; }
}
