using Events;
using Events.Args;
using Messages.Handling.Args;

namespace Chamber.ConsoleApp;

public class Logger
{
    public Logger()
    {
        PriorityEventHandler.Subscribe<ErrorArgs>(OnError, 1);
        PriorityEventHandler.Subscribe<MessageRecievedArgs>(OnMessage, 1);
    }

    private void OnMessage(MessageRecievedArgs args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Пришло сообщение в {DateTime.Now}, Текст: {args.Message.Text ?? "Unknown"}");
        Console.ResetColor();
    }

    private void OnError(ErrorArgs args)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(args.Exception.Message);
        Console.ResetColor();
    }

}
