using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Chamber.Processes;

[JsonObject]
public interface IOneActProcess : IProcess
{
    void NextAction(Message message);
}
