using Chamber.Dialogs.FieldRequestDialog;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

public interface ISolutionProcess:IMultiActProcess
{
    List<IDataProcess> Processes { get; set; }
}
