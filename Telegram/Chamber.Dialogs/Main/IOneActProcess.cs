using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.Main;

[JsonObject]
public interface IOneActProcess : IProcess
{
    void NextAction(Message message);
}
