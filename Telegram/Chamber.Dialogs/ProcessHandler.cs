using Chamber.Collections;
using Chamber.Dialogs;
using Events;
using Events.Args;
using Telegram.Bot.Types;

namespace Chamber.Support.Types;

public static class ProcessHandler
{
    public static ExecutingProcessCollection Processec => new();

    public static void Run(long id, IProcess process)
    {
        try
        {
            if (process is UnknownProcess)
            {
                return;
            }



            ExecutingProcess? executingProcess = Processec.Find(i => i.Id == id);

            if (executingProcess == null)
            {
                executingProcess = new(id, process);
                executingProcess.Start();
                Processec.Add(executingProcess);
                Processec.Refresh();
                return;
            }

            executingProcess.StartProcess = process;
            executingProcess.Start();
            Processec.Refresh();
            return;
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

            if (proc == null)
            {
                return false;
            }
            proc.NextAction(message);

            PriorityEventHandler.Invoke(new LogMessage("Вызвался метод NextAction / ProcessHandler "));
            Processec.Refresh();
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
