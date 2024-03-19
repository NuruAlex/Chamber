using Chamber.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chamber.Core.Requests;

[Serializable]
public class HumanRequest(long id) : Request(id)
{
    public DateTime DoneTime { get; set; }
    public int Level { get; set; }
    public string Description { get; set; }
    public string? FilePath { get; set; }
    public TelegramUser? Executor { get; set; }
}
