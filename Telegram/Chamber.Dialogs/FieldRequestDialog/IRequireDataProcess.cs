using Chamber.Core.Requests;
using Chamber.Dialogs.Main;

namespace Chamber.Processes.FieldRequestProcesses;

public interface IRequireDataProcess : IOneActProcess
{
    bool WasDone { get; set; }
    BotRequest SetSpecificValue(BotRequest request);
}
