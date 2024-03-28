using Chamber.Collections;
using Chamber.Core.Requests;
using Chamber.Dialogs.Main;

namespace Chamber.Dialogs.SpecialistDialogs;

[Serializable]
public class PrintSpecialistRequests : IProcess
{
    public void Start()
    {
        List<Request> requests = DataBase.Requests.Items;

        foreach (Request request in requests)
        {
            
        }
    }
}
