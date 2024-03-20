using Chamber.Core.Requests;
using Chamber.Core.Users;

namespace Chamber.Serialization;

internal static class FileArchieve
{
    
    private static readonly Dictionary<Type, string> _pathByType = new()
    {
        {
            typeof(TelegramUser),"Resources\\Serialization\\Users"
        },
        {
            typeof(Request),"Resources\\Serialization\\Requests"
        }
    };

    public static string PathByType<T>()
    {
        return _pathByType[typeof(T)];
    }
}