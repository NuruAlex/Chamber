using Chamber.Core.Users;
using Messages.Core.Reply.Markups;
using Messages.Senders.InnerSenders;

namespace Messages.Senders.Extensions;

public static class TextSenderExtension
{
    public static async Task<int> SendMessage(this TextSender sender, Client client, string text, IMarkup markup)
    {
        return await sender.SendMessage(client.Id, text, markup);
    }

}
