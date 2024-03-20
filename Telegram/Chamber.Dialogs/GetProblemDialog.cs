using Chamber.Core.Users;

namespace Chamber.Dialogs;

public class GetProblemDialog(Client client, string problemName) : IStartDialog
{
    private readonly Client Client = client;
    private readonly string problemName = problemName;
    public async void Start()
    {
    }
}
