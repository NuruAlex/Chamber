namespace Chamber.Dialogs;

public interface IMultiActProcess : IOneActProcess
{
    int Iteration { get; set; }
}
