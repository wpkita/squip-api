namespace Squip.Rest.Services;

public class DevUserIdProvider : IUserIdProvider
{
    public string GetCurrentUserId()
    {
        return "anonymous";
    }
}
