using Telegram.Bot.Types;

namespace Chamber.Processes.ValidationProcesses;

[Serializable]
public class ValidateRequestId : IValidationProcess
{
    public bool IsValid(Message message)
    {
        if (message.Text == null)
        {
            return false;
        }
        //regex
        return true;
    }
}
