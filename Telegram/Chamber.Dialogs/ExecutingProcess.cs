using Chamber.Core;
using Chamber.Support.Types;
using Telegram.Bot.Types;

namespace Chamber.Dialogs;

[Serializable]
public class ExecutingProcess(long chatId, IProcess process) : BaseEntity(chatId)
{
    public IProcess StartProcess { get; set; } = process;

    public void Start()
    {
        StartProcess.Start();
    }

    public void NextAction(Message message)
    {
        if (message == null)
        {
            return;
        }

        if (StartProcess == null || StartProcess is not IOneActProcess process)
        {
            return;
        }

        process.NextAction(message);
        ProcessHandler.Processec.Refresh();     
    }
}
