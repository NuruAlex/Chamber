using Telegram.Bot.Types;

namespace Chamber.Dialogs;

public interface IOneActProcess : IProcess
{
    void NextAction(Message message);
}
