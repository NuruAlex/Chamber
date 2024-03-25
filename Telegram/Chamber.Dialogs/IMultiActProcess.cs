using Newtonsoft.Json;

namespace Chamber.Dialogs;

[JsonObject]
public interface IMultiActProcess : IOneActProcess
{
    int Iteration { get; set; }
}
