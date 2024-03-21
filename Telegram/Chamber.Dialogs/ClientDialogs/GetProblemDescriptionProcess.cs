using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.FrequentlyProblems;
using Chamber.Core.Users;
using Events;
using Events.Args;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class GetProblemDescriptionProcess(Client client, string problemType) : IClientProcess
{
    public Client Client { get; set; } = client;
    public string ProblemType { get; set; } = problemType;

    public async void Start()
    {
        FrequentlyProblem? problem = DataBase.Problems.Find(i => i.Title == ProblemType);

        if (problem == null)
        {
            PriorityEventHandler.Invoke(new ErrorArgs(new Exception("GetProblemDescriptionProcess, not found")));
            return;
        }


        await Sender.SendMessage(new TextMessage(Client.Id, problem.Solution)
        {
            Markup = new InlineMarkup(
                 new InlineButton("Помогло ли вам?",
                     new CallBackPacket(Client.Id, CallBackCode.ItHelped, sendData: ProblemType).Pack()),

                 new InlineRow(),

                 new InlineButton("Назад",
                     new CallBackPacket(Client.Id, CallBackCode.PrintProblemTypes).Pack()))
        });
    }
}
