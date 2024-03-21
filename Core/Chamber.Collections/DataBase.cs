using Chamber.Serialization;

namespace Chamber.Collections;

public static class DataBase
{
    public static DataRetriever Serializer = new();

    
    private static UserCollection? _users;

    private static RequestCollection? _requests;

    private static FrequentlyProblemCollection? _problems;

   
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

    public static FrequentlyProblemCollection Problems
    {
        get
        {
            return _problems ??= new();
        }
    }


}
