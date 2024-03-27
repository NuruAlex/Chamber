using Chamber.Core.Users;
using Chamber.Processes.FieldRequestProcesses;
using Chamber.Processes.ProblemSolutionDialogs;
using Chamber.Support.Types;

namespace Chamber.Processes.ClientDialogs;

[Serializable]
public class GetProblemSolutionDialog(Client client, string problemTitle) : IProcess
{
    public Client Client { get; set; } = client;
    public string ProblemType { get; set; } = problemTitle;

    public void Start()
    {
        List<IRequireDataProcess>? solutionProcess = SolutionArchieve.GetSolutionProcesses(Client, ProblemType);

        if (solutionProcess == null || solutionProcess.Count == 0)
        {
            return;
        }

        ProcessHandler.Run(Client.Id, new ExecutingSolutionProcess(Client, solutionProcess));
    }
}
