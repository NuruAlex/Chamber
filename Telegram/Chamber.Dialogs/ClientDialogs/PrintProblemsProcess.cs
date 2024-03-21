using Chamber.CallBack.Types;
using Chamber.Collections;
using Chamber.Core.FrequentlyProblems;
using Chamber.Core.Users;
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
        InlineMarkup markup = new(new InlineButton("Не типовая",
            new CallBackPacket(Client.Id, CallBackCode.NonTypeProblem).Pack()),
            new InlineRow());


        foreach (FrequentlyProblem problem in DataBase.Problems.Items)
        {
            markup.AddButton(new InlineButton(problem.Title,
                new CallBackPacket(Client.Id, CallBackCode.GetProblemType, sendData: problem.Title).Pack()))
                .AddRow();
        }

        markup.AddButton(new InlineButton("Назад", new CallBackPacket(Client.Id, CallBackCode.ClientMainMenu).Pack()));
        await Sender.SendMessage(new TextMessage(Client.Id, "Выберите тип проблемы", markup));
    }
}
