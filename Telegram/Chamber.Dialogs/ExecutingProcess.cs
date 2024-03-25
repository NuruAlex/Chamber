using Chamber.Core;
using Events;
using Events.Args;
using Telegram.Bot.Types;

namespace Chamber.Dialogs;

[Serializable]
public class ExecutingProcess : BaseEntity
{
    public IProcess StartProcess { get; set; }

    public ExecutingProcess(long chatId, IProcess process) : base(chatId)
    {
        StartProcess = process;
    }

    public void Start()
    {
        StartProcess?.Start();
    }

    public void NextAction(Message message)
    {
        try
        {
            if (StartProcess is IOneActProcess oneActProcess)
            {
                oneActProcess.NextAction(message);
                PriorityEventHandler.Invoke(new LogMessage("ExecutingProcess / NextAction"));
            }
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(ex));
        }

    }
}
