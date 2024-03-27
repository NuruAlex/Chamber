using Telegram.Bot.Types;

namespace Chamber.Processes.ValidationProcesses;
[Serializable]
public class ValidateBlankProcess : IValidationProcess
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
