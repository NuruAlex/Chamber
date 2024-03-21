using Chamber.Core.Users;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class CreateHumanRequestProcess(Client client) : IClientMultiActProcess
{
    public Client Client { get; set; } = client;
    public int Iteration { get; set; } = -1;

    public void NextAction(Message message)
    {

    }

    public void Start()
    {

    }
}
