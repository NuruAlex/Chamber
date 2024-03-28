using Chamber.Core.Enums;

namespace Chamber.Recievers.Archieves;

public static class UserLevelAchieve
{
    private static readonly Dictionary<string, UserLevel> _levels = new()
    {
        {"/asadmin",UserLevel.Admin},
        {"/asspecialist",UserLevel.Specialist},
        {"/asclient",UserLevel.Client},
        {"/asprogrammist",UserLevel.Programmist},

    };


    public static UserLevel? GetUserLevel(string command)
    {
        try
        {
            return _levels[command];
        }
        catch (Exception)
        {
            return null;
        }
    }
}
