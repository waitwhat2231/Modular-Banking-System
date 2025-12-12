using Common.SharedClasses.Pagination;
using Modules.Users.Application.Dtos;

namespace Common.SharedClasses.Services
{
    public interface IUsersService
    {
        public Task<UserDto> GetUserById(string userId);
        public Task<PagedEntity<MiniUserDto>> GetAllUsersAsync(int page, int pageSize, string userName);
        Task<List<MiniUserDto>> GetAllUsersNoPagination(string userName);
    }
}
