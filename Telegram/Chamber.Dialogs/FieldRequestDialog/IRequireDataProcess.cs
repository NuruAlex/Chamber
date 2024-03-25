namespace Chamber.Dialogs.FieldRequestDialog;

public interface IRequireDataProcess : IOneActProcess
{
    bool WasDone { get; set; }
}
