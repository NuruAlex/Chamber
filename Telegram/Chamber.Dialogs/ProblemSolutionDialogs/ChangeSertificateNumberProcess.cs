using Chamber.Core.Users;
using Chamber.Dialogs.ClientDialogs;
using Chamber.Dialogs.FieldRequestDialog;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.ProblemSolutionDialogs;

[Serializable]
public class ChangeSertificateNumberProcess : ISolutionProcess
{
    public Client Client { get; set; }
    public List<IDataProcess> Processes { get; set; }

    public ChangeSertificateNumberProcess(Client client)
    {
        Client = client;
        Processes =
        [
            new RequireRequestNumberProcess(client),
            new RequireNewSetrificateNumber(client)
        ];
    }

    public int Iteration { get; set; }

    public long NewSetrificateNumber { get; set; }

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
