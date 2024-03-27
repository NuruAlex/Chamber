using Newtonsoft.Json;

namespace Chamber.Processes;

[JsonObject]
public interface IMultiActProcess : IOneActProcess
{
    int Iteration { get; set; }
}
