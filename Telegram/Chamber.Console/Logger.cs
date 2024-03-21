using Events;
using Events.Args;
using Messages.Handling.Args;

namespace Chamber.ConsoleApp;

public static class Logger
{
    public static void Init()
    {
        PriorityEventHandler.Subscribe<ErrorArgs>(OnError, 1);
        PriorityEventHandler.Subscribe<MessageRecievedArgs>(OnMessage, 1);
    }

    private static void OnMessage(MessageRecievedArgs args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Пришло сообщение в {DateTime.Now}, Текст: {args.Message.Text ?? "Unknown"}");
        Console.ResetColor();
    }

    private static void OnError(ErrorArgs args)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(args.Exception.Message);
        Console.ResetColor();
    }

}
