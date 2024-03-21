using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class CreateBotRequestProcess(Client client, string problemType) : IClientProcess
{
    public Client Client { get; set; } = client;
    public string ProblemType { get; set; } = problemType;

    public async void Start()
    {
        int id = DataBase.Requests.Count + 1;

        DataBase.Requests.Add(new BotRequest(id, Client, ProblemType));
        await Sender.SendMessage(new TextMessage(Client.Id, "Спасибо за обращение"));
        new PrintMainMenuDialog(Client).Start();
    }
}
