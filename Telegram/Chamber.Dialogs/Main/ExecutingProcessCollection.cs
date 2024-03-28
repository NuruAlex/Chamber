using Chamber.Collections;

namespace Chamber.Dialogs.Main;

public class ExecutingProcessCollection : UniqeCollection<ExecutingProcess>
{
    public override bool Contains(ExecutingProcess item)
    {
        return Contains(i => i.Id == item.Id);
    }
    public bool Contains(long chatId)
    {
        return Contains(i => i.Id == chatId);
    }

    public override void Delete(ExecutingProcess item)
    {
        Delete(i => i.Id == item.Id);
    }
}
