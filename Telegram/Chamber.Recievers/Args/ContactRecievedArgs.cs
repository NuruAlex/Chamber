using Telegram.Bot.Types;

namespace Chamber.Recievers.Args;

public class ContactRecievedArgs(long chatId, Contact contact)
{
    public readonly long ChatId = chatId;
    public readonly Contact Contact = contact;
}
