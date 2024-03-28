using Chamber.CallBack.Types;
using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Dialogs.Main;
using Chamber.Processes.ClientDialogs;
using Events;
using Events.Args;

namespace Chamber.Recievers.Archieves;

public static class CallBackDialogArchieve
{
    private static Dictionary<CallBackCode, IProcess> _clientProcesses = [];

    private static void Update(Client client, CallBackPacket packet)
    {
        _clientProcesses = new()
        {
            { CallBackCode.NonTypeProblem,new CreateHumanRequestProcess(client) },
            { CallBackCode.GetSolution,   new GetProblemSolutionDialog(client, packet.SendData) },
            { CallBackCode.PrintProblemTypes,new PrintProblemsProcess(client) },
            { CallBackCode.ClientMainMenu, new PrintMainMenuDialog(client) },
            { CallBackCode.MyRequests, new PrintMyRequests(client) },
            { CallBackCode.GetRequest, new PrintRequestDialog(client,packet.SendData) },
        };
    }

    public static IProcess GetClientProcess(Client client, CallBackPacket packet)
    {
        try
        {
            Update(client, packet);
            return _clientProcesses[packet.Code];
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
            return new UnknownProcess();
        }
    }
}
