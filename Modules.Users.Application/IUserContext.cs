namespace Modules.Users.Application
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
        string? GetAccessToken();
    }
}
