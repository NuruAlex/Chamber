namespace Chamber.Dialogs.FieldRequestDialog;

public interface IDataProcess : IOneActProcess
{
    bool WasDone { get; set; }
}
