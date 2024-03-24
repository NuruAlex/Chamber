using Chamber.Core.Users;
using Chamber.Dialogs.ProblemSolutionDialogs;
using Chamber.Support.Types;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class GetProblemSolutionDialog(Client client, string problemTitle) : IProcess
{
    public Client Client { get; set; } = client;
    public string ProblemType { get; set; } = problemTitle;

    public void Start()
    {
        ISolutionProcess? solutionProcess = SolutionArchieve.GetSolution(Client, ProblemType);

        if (solutionProcess == null)
        {
            return;
        }

        ProcessHandler.Run(Client.Id, solutionProcess);
    }
}
