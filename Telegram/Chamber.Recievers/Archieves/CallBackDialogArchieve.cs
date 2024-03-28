using Chamber.CallBack.Types;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Dialogs.AdminDialogs;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Dialogs.Main;
using Chamber.Dialogs.SpecialistDialogs;
using Chamber.Processes.ClientDialogs;
using Events;
using Events.Args;

namespace Chamber.Recievers.Archieves;

public static class CallBackDialogArchieve
{
    private static Dictionary<CallBackCode, IProcess> _processes = [];

    private static void Update(TelegramUser user, CallBackPacket packet)
    {
        if (user.CurrentLevel == UserLevel.Admin)
        {
            _processes = new()
            {
                { CallBackCode.MainMenu,new PrintAdminMainMenu(user) },
               
                { CallBackCode.PrintClient,new PrintClientDialog(user,packet.SendData) },
                { CallBackCode.PrintClients,new PrintClientsList(user) },
                { CallBackCode.MakeNewSpecialist,new MakeSpecialistProcess(user,packet.SendData) },
                { CallBackCode.MakeNewAdmin,new MakeNewAdminProcess(user,packet.SendData) },
                { CallBackCode.MakeNewProgrammist,new MakeNewProgrammist(user,packet.SendData) }
            };
        }
        else if (user.CurrentLevel == UserLevel.Client)
        {
            _processes = new()
            {
                { CallBackCode.MainMenu, new PrintClientMainMenuDialog(user) },
               
                { CallBackCode.NonTypeProblem,new CreateHumanRequestProcess(user) },
                { CallBackCode.GetSolution,   new GetProblemSolutionDialog(user, packet.SendData) },
                { CallBackCode.PrintProblemTypes,new PrintProblemsProcess(user) },
                { CallBackCode.MyRequests, new PrintMyRequests(user) },
                { CallBackCode.GetRequest, new PrintRequestDialog(user,packet.SendData) },
            };
        }
        else if (user.CurrentLevel == UserLevel.Specialist)
        {
            _processes = new()
            {
                {CallBackCode.MainMenu,new PrintSpecialistMainMenu(user) }
            };
        }
        else
        {
            _processes = [];
        }
    }


    public static IProcess GetProcess(TelegramUser client, CallBackPacket packet)
    {
        try
        {
            Update(client, packet);
            return _processes[packet.Code];
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
            return new UnknownProcess();
        }
    }

}