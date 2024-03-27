using Chamber.Core.Requests;

namespace Chamber.Dialogs.FieldRequestDialog;

public interface IRequireDataProcess : IOneActProcess
{
    bool WasDone { get; set; }
    BotRequest SetSpecificValue(BotRequest request);
}
