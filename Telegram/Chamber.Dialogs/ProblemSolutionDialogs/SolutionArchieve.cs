using Chamber.Core.Users;
using Chamber.Processes.FieldRequestProcesses;
using Events;
using Events.Args;

namespace Chamber.Processes.ProblemSolutionDialogs;

public static class SolutionArchieve
{
    private static Dictionary<string, List<IRequireDataProcess>> _solutionDialogs = [];

    private static void Update(TelegramUser client)
    {
        _solutionDialogs = new()
        {
            { "Заменить № сертифика", [
                new RequireRequestNumberProcess(client),
                new RequireNewCetrificateNumberProcess(client)]
            },
            { "Заменить № бланка", [
                new RequireRequestNumberProcess(client),
                new RequireOldBlankNumber(client),
                new RequireNewBlankNumberProcess(client)]
            },
        };
    }

    public static List<string> GetNames(TelegramUser client)
    {
        Update(client);

        return [.. _solutionDialogs.Keys];
    }

    public static List<IRequireDataProcess>? GetSolutionProcesses(TelegramUser client, string problemName)
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
