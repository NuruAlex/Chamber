using Chamber.Serialization;

namespace Chamber.Collections;

public static class DataBase
{
    private static DataRetriever? _retriever;
    public static DataRetriever Serializer
    {
        get
        {
            return _retriever ??= new();
        }
    }


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
