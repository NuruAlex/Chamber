using Chamber.Core.Users;

namespace Chamber.Collections;

public class UserCollection : UniqeCollection<TelegramUser>
{
    public override bool Contains(TelegramUser item)
    {
        return Contains(i => i.Id == item.Id);
    }

    public override void Delete(TelegramUser item)
    {
        Delete(i => i.Id == item.Id);
    }
}
