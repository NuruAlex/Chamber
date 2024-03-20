using Chamber.Serialization;

namespace Chamber.Collections;

public static class DataBase
{
    public static DataRetriever Serializer = new();

    private static UserCollection? _users;

    private static RequestCollection? _requests;


    public static UserCollection Users
    {
        get
        {
            return _users ??= new();
        }
    }

    public static RequestCollection Requests
    {
        get
        {
            return _requests ??= new();
        }
    }
}
