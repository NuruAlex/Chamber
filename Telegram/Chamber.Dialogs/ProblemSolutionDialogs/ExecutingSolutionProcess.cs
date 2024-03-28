using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Core.Users;
using Chamber.Processes.ClientDialogs;
using Chamber.Processes.FieldRequestProcesses;
using Chamber.Support.Types;
using Messages.Core.Types;
using Messages.Senders;
using Telegram.Bot.Types;

namespace Chamber.Processes.ProblemSolutionDialogs;

[Serializable]
public class ExecutingSolutionProcess(Client client, List<IRequireDataProcess> processes, string problemTitle) : ISolutionProcess
{
    public List<IRequireDataProcess> Processes { get; set; } = processes;
    public Client Client { get; } = client;

    public int Iteration { get; set; } = 0;

    public BotRequest BotRequest { get; set; } = new(DataBase.Requests.Count + 1, client, problemTitle);

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

        if (Iteration <= Processes.Count - 1)
        {
            BotRequest = Processes[Iteration].SetSpecificValue(BotRequest);
        }

        if (Iteration < Processes.Count - 1)
        {
            Iteration++;
            Processes[Iteration].Start();
            return;

        }

        if (Iteration == Processes.Count - 1)
        {
            DataBase.Requests.Add(BotRequest);

            await Sender.SendMessage(
                new TextMessage(Client.Id,
                    "Спасибо за обращение, введенные вами данные отправлены специалисту на обработку"));

            ProcessHandler.Run(Client.Id, new PrintMainMenuDialog(Client));
        }
    }

    public async void Start()
    {
        if (Processes.Count == 0)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Процессы не найдены"));
            ProcessHandler.Run(Client.Id, new PrintMainMenuDialog(Client));
        }

        Processes[Iteration].Start();
    }
}
