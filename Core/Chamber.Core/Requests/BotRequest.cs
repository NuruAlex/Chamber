using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class BotRequest(long id, TelegramUser client, string name) : Request(id, client, name)
{
    public string? RequestId { get; set; }
    public string? OldCertificate { get; set; }
    public string? NewCertificate { get; set; }
    public string? OldBlank { get; set; }
    public string? NewBlank { get; set; }

    public override string ToText()
    {
        string result = "";

        if (RequestId != null)
        {
            result += $"Номер заявки: {RequestId}\n";
        }

        if (OldCertificate != null)
        {
            result += $"Старый номер сертификата: {OldCertificate}\n";
        }

        if (NewCertificate != null)
        {
            result += $"Новый номер сертификата: {NewCertificate}\n";
        }

        if (OldBlank != null)
        {
            result += $"Старый номер бланка: {OldBlank}\n";
        }

        if (NewBlank != null)
        {
            result += $"Новый номер бланка: {NewBlank}\n";
        }

        result += Creation.ToString("yyyy-MM-dd hh:mm:ss");

        return result;
    }

    public BotRequest Copy()
    {
        return new BotRequest(Id,Client, Name)
        {
            RequestId = RequestId,
            OldCertificate = OldCertificate,
            NewCertificate = NewCertificate,
            OldBlank = OldBlank,
            NewBlank = NewBlank,
        };
    }
}
