using Chamber.Core.Requests;

namespace Chamber.Collections;

public class RequestCollection : UniqeCollection<Request>
{
    public override bool Contains(Request item)
    {
        return Contains(i => i.Id == item.Id);
    }

    public override void Delete(Request item)
    {
        Delete(i => i.Id == item.Id);
    }
}
