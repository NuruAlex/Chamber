﻿using Chamber.Core.Users;
using Chamber.Dialogs.FieldRequestDialog;
using Events;
using Events.Args;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

public static class SolutionArchieve
{
    private static Dictionary<string, List<IRequireDataProcess>> _solutionDialogs = [];

    private static void Update(Client client)
    {
        _solutionDialogs = new()
        {
            { "Заменить № сертифика", [
                new RequireRequestNumberProcess(client),
                new RequireNewSetrificateNumber(client)]
            },
            { "Заменить № бланка", [
                new RequireRequestNumberProcess(client),
                new RequireOldBlankNumber(client),
                new RequireNewBlankNumberProcess(client)]
            },
        };
    }

    public static List<string> GetNames(Client client)
    {
        Update(client);

        return [.. _solutionDialogs.Keys];
    }

    public static List<IRequireDataProcess>? GetSolutionProcesses(Client client, string problemName)
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
