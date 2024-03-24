using Chamber.CallBack.Types;
using Chamber.Core.Users;
using Chamber.Dialogs.ProblemSolutionDialogs;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintProblemsProcess(Client client) : IProcess
{
    public Client Client { get; set; } = client;

    public async void Start()
    {
        List<string> problemNames = SolutionArchieve.GetNames(Client);

        InlineMarkup markup = new(new InlineButton("Другая проблема",
            new CallBackPacket(Client.Id, CallBackCode.NonTypeProblem)),
            new InlineRow());


        foreach (string problem in problemNames)
        {
            markup.AddButton(new InlineButton(problem,
                new CallBackPacket(Client.Id, CallBackCode.GetSolution, sendData: problem)))
                .AddRow();
        }

        markup.AddButton(new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.ClientMainMenu)));

        await Sender.SendMessage(new TextMessage(Client.Id, "Выберите тип проблемы", markup));
    }
}
