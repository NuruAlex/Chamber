using Chamber.CallBack.Types;
using Chamber.Core.Users;
using Chamber.Dialogs.ProblemSolutionDialogs;
using Events;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Dialogs.ClientDialogs;

[Serializable]
public class PrintProblemsProcess(Client client) : IClientProcess
{
    public Client Client { get; set; } = client;

    public async void Start()
    {
        List<string> problemNames = SolutionArchieve.GetNames(Client);

        InlineMarkup markup = new(new InlineButton("Другая проблема",
            new CallBackPacket(Client.Id, CallBackCode.NonTypeProblem).Pack()),
            new InlineRow());


        foreach (string problem in problemNames)
        {
            markup.AddButton(new InlineButton(problem,
                new CallBackPacket(Client.Id, CallBackCode.GetSolution, sendData: problem).Pack()))
                .AddRow();
        }

        markup.AddButton(new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.ClientMainMenu).Pack()));
        try
        {
            markup.ToMarkup();
        }
        catch (Exception ex)
        {
            PriorityEventHandler.Invoke(new ErrorEventArgs(ex));
        }

        await Sender.SendMessage(new TextMessage(Client.Id, "Выберите тип проблемы", markup));
    }
}
