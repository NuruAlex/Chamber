using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Dialogs.FieldRequestDialog;
using Messages.Core.Types;
using Messages.Senders;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

[JsonObject]
public class ChangeSertificateNumberProcess(Client client) : ISolutionProcess
{
    public Client Client { get; set; } = client;
    public List<IDataProcess> Processes { get; set; } =
        [
            new RequireRequestNumberProcess(client),
            new RequireNewSetrificateNumber(client)
        ];

    [JsonProperty]
    public int Iteration { get; set; }
   
    [JsonProperty]
    public long NewSetrificateNumber { get; set; }

    [JsonProperty]
    public long RequestNumber { get; set; }

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
            NewSetrificateNumber = ((RequireNewSetrificateNumber)Processes[Iteration]).NewCertificateNumber;

            await Sender.SendMessage(
                new TextMessage(Client.Id,
                    "Спасибо за обращение, введенные вами данные отправлены специалисту на обработку"));


            new PrintMainMenuDialog(Client).Start();
        }
    }

    public void Start()
    {
        Processes[Iteration].Start();
    }
}
