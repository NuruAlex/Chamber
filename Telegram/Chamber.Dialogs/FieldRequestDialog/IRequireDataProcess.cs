using Chamber.Core.Requests;

namespace Chamber.Processes.FieldRequestProcesses;

public interface IRequireDataProcess : IOneActProcess
{
    bool WasDone { get; set; }
    BotRequest SetSpecificValue(BotRequest request);
}
