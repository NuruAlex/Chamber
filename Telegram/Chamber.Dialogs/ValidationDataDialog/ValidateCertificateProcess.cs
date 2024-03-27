using Telegram.Bot.Types;

namespace Chamber.Processes.ValidationProcesses;

[Serializable]
public class ValidateCertificateProcess : IValidationProcess
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
