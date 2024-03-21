using Chamber.Core.Users;

namespace Chamber.Dialogs.ClientDialogs;

public interface IClientProcess : IProcess
{
    Client Client { get; set; }
}
