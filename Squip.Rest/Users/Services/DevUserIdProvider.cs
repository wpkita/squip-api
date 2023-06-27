namespace Squip.Rest.Users.Services;

public class DevUserIdProvider : IUserIdProvider
{
    public string GetCurrentUserId()
    {
        return "anonymous";
    }
}
