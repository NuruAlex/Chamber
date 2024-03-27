using Chamber.Collections;
using Chamber.Processes;
using Events;
using Events.Args;
using Telegram.Bot.Types;

namespace Chamber.Support.Types;

public static class ProcessHandler
{
    private static readonly ExecutingProcessCollection? _processes = new();
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
            TerminateProcess(id);

            ExecutingProcess executingProcess = new(id, process);

            Processec.Add(executingProcess);

            executingProcess.Start();
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
            ExecutingProcess? process = Processec.Find(i => i.Id == chatId);

            if (process != null)
            {
                process.NextAction(message);
                Processec.Refresh();
            }

            return process?.StartProcess != null && process?.StartProcess is IOneActProcess;
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
            return false;
        }
    }


    public static void TerminateProcess(long id)
    {
        if (Processec.Contains(id))
        {
            Processec.Delete(i => i.Id == id);
        }
    }
}
