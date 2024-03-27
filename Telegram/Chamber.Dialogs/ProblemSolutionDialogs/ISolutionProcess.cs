using Chamber.Processes.FieldRequestProcesses;

namespace Chamber.Processes.ProblemSolutionDialogs;

public interface ISolutionProcess : IMultiActProcess
{
    List<IRequireDataProcess> Processes { get; set; }
}
