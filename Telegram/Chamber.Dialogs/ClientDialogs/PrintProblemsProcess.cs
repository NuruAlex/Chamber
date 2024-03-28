using Chamber.CallBack.Types;
using Chamber.Core.Users;
using Chamber.Dialogs.Main;
using Chamber.Processes.ProblemSolutionDialogs;
using Chamber.Support.Types;
using Messages.Core.Reply.Buttons;
using Messages.Core.Reply.Markups;
using Messages.Core.Reply.Rows;
using Messages.Core.Types;
using Messages.Senders;

namespace Chamber.Processes.ClientDialogs;

[Serializable]
public class PrintProblemsProcess(TelegramUser client) : IProcess
{
    public TelegramUser Client { get; set; } = client;

    public async void Start()
    {
        if (Client.CurrentLevel != Core.Enums.UserLevel.Client)
        {
            ProcessHandler.TerminateProcess(Client.Id);
            return;
        }


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

        markup.AddButton(new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.MainMenu)));

        await Sender.SendMessage(new TextMessage(Client.Id, "Выберите тип проблемы", markup));
    }
}
