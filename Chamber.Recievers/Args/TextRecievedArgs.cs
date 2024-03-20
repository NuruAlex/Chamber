namespace Chamber.Recievers.Args;

public class TextRecievedArgs(long chatId, string text)
{
    public readonly long ChatId = chatId;
    public readonly string Text = text;
}
