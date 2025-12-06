using Common.SharedClasses.Dtos.Users;

namespace Common.SharedClasses.Services;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
    string? GetAccessToken();
}
