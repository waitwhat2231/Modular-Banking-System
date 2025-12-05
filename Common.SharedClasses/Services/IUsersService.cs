using Modules.Users.Application.Dtos;

namespace Common.SharedClasses.Services
{
    public interface IUsersService
    {
        public Task<UserDto> GetUserById(string userId);
    }
}
