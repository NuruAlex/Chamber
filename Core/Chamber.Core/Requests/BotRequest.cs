using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public class BotRequest(long id, Client client) : Request(id, client)
{
    public long? RequestId { get; set; }
    public long? OldCertificate { get; set; }
    public long? NewCertificate { get; set; }
    public long? OldBlank { get; set; }
    public long? NewBlank { get; set; }

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

        return result;
    }

    public BotRequest Copy()
    {
        return new BotRequest(Id, Client)
        {
            RequestId = RequestId,
            OldCertificate = OldCertificate,
            NewCertificate = NewCertificate,
            OldBlank = OldBlank,
            NewBlank = NewBlank,

        };
    }
}
