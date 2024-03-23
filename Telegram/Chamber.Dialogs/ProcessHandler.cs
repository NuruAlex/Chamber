using Chamber.Collections;
using Chamber.Dialogs;
using Events;
using Events.Args;
using Telegram.Bot.Types;

namespace Chamber.Support.Types;

public static class ProcessHandler
{
    private static readonly ExecutingProcessCollection? _processes;
    public static ExecutingProcessCollection Processec
    {
        get
        {
            return _processes ?? new();
        }
    }

    public static void Run(long id, IProcess process)
    {
        if (process is UnknownProcess)
        {
            return;
        }

        try
        {
            ExecutingProcess? executingProcess = Processec.Find(i => i.Id == id);

            if (executingProcess == null)
            {
                executingProcess = new(id, process);
                executingProcess.Start();
                Processec.Add(executingProcess);
            }
            else
            {
                executingProcess.StartProcess = process;
                executingProcess.Start();
            }

            Processec.Refresh();
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
        }
    }


    public static IProcess? GetProcess(long chatId)
    {
        ExecutingProcess? process = Processec.Find(i => i.Id == chatId);

        return process?.StartProcess ?? null;
    }

    public static bool NextAction(long chatId, Message message)
    {
        try
        {
            ExecutingProcess? proc = Processec.Find(i => i.Id == chatId);

            if (proc != null)
            {
                proc.NextAction(message);
                Processec.Refresh();
            }

            return proc?.StartProcess != null && proc?.StartProcess is IOneActProcess;
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
            return false;
        }
    }


    public static void StopProcess(long id)
    {
        if (Processec.Contains(id))
        {
            Processec.Delete(i => i.Id == id);
        }
    }
}
