using Chamber.Core;
using Events;
using Events.Args;
using Telegram.Bot.Types;

namespace Chamber.Processes;

[Serializable]
public class ExecutingProcess(long chatId, IProcess process) : BaseEntity(chatId)
{
    public IProcess StartProcess { get; set; } = process;

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

    public override string ToText()
    {
        throw new NotImplementedException();
    }
}
