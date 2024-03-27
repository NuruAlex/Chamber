using Telegram.Bot.Types;

namespace Chamber.Processes.ValidationProcesses;

public interface IValidationProcess
{
    bool IsValid(Message message);
}
