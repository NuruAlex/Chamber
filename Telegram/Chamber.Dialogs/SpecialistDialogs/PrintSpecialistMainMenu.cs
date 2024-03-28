using Chamber.Core.Users;
using Chamber.Dialogs.Main;

namespace Chamber.Dialogs.SpecialistDialogs;

[Serializable]
public class PrintSpecialistMainMenu(Specialist specialist) : IProcess
{
    public Specialist Specialist { get; set; } = specialist;
   
    public void Start()
    {
    }
}
