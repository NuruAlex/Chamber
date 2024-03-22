using Chamber.Core.Users;
using Events;
using Events.Args;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

public static class SolutionArchieve
{
    private static Dictionary<string, ISolutionProcess> _solutionDialogs = new();

    private static void Update(Client client)
    {
        _solutionDialogs = new()
        {
            { "Заменить н с", new ChangeSertificateNumberProcess(client) },
        };
    }

    public static List<string> GetNames(Client client)
    {
        Update(client);

        return [.. _solutionDialogs.Keys];
    }

    public static ISolutionProcess? GetSolution(Client client, string problemName)
    {
        Update(client);

        try
        {
            return _solutionDialogs[problemName];
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
            return null;
        }
    }
}
