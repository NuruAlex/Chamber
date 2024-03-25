using Chamber.Dialogs.FieldRequestDialog;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

public interface ISolutionProcess : IMultiActProcess
{
    List<IRequireDataProcess> Processes { get; set; }
}
