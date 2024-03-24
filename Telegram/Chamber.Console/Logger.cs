using Events;
using Events.Args;
using Messages.Handling.Args;

namespace Chamber.Log;

public static class Logger
{
    public static void Init()
    {
        PriorityEventHandler.Subscribe<ErrorArgs>(OnError, 1);
        PriorityEventHandler.Subscribe<MessageRecievedArgs>(OnMessage, 1);
        PriorityEventHandler.Subscribe<LogMessage>(OnLogMessage, 1);
    }
    public static void LogMessage(string message)
    {
        PriorityEventHandler.Invoke(new LogMessage(message));
    }

    private static void OnLogMessage(LogMessage message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Пришло сообщение в {DateTime.Now}, Текст: {message.Message}");
        Console.ResetColor();
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
