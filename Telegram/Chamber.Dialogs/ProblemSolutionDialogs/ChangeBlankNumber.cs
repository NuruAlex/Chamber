using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Dialogs.FieldRequestDialog;
using Chamber.Support.Types;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

[Serializable]
public class ChangeBlankNumber(Client client) : ISolutionProcess
{
    public Client Client { get; set; } = client;

    public List<IRequireDataProcess> Processes { get; set; } =
    [
        new RequireRequestNumberProcess(client),
        new RequireOldBlankNumber(client),
        new RequireNewBlankNumberProcess(client)
    ];

    public long? RequestNumber { get; set; }
    public long? OldBlankNumber { get; set; }
    public long? NewBlankNumber { get; set; }
    public int Iteration { get; set; }

    public async void NextAction(Message message)
    {
        if (!Processes[Iteration].WasDone)
        {
            Processes[Iteration].NextAction(message);
        }

        if (!Processes[Iteration].WasDone)
        {
            return;
        }

        if (Iteration == 0)
        {
            RequestNumber = ((RequireRequestNumberProcess)Processes[Iteration]).RequestNumber;
            Iteration++;
            Processes[Iteration].Start();
            return;
        }

        if (Iteration == 1)
        {
            OldBlankNumber = ((RequireOldBlankNumber)Processes[Iteration]).OldBlankNumber;
            Iteration++;
            Processes[Iteration].Start();
            return;
        }

        if (Iteration == 2)
        {
            NewBlankNumber = ((RequireNewBlankNumberProcess)Processes[Iteration]).NewBlankBumber;

            int requestId = DataBase.Requests.Count + 1;

            BotRequest request = new(requestId, Client)
            {
                RequestId = RequestNumber,
                OldBlank = OldBlankNumber,
                NewBlank = NewBlankNumber,
            };

            DataBase.Requests.Add(request);
            await Sender.SendMessage(new TextMessage(Client.Id, "Обращение отправлено на обработку"));
            ProcessHandler.Run(Client.Id, new PrintMainMenuDialog(Client));
        }
    }

    public void Start()
    {
        Processes[Iteration].Start();
    }
}
