using AutoMapper;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Services;
using Modules.Users.Application.Dtos;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.Services
{
    class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<PagedEntity<MiniUserDto>> GetAllUsersAsync(int page, int pageSize, string userName)
        {
            var pagedEntity = await _usersRepository.GetAllPaginatedAsync(page, pageSize, userName);

            var resultPaginated = new PagedEntity<MiniUserDto>();
            resultPaginated.Items = _mapper.Map<List<MiniUserDto>>(pagedEntity.Items);
            resultPaginated.TotalItems = pagedEntity.TotalItems;
            resultPaginated.PageNumber = pagedEntity.PageNumber;
            resultPaginated.PageSize = pagedEntity.PageSize;

            return resultPaginated;
        }
        public async Task<List<MiniUserDto>> GetAllUsersNoPagination(string userName)
        {
            var users = await _usersRepository.GetAllUsersNotPaginated(userName);
            var result = _mapper.Map<List<MiniUserDto>>(users);
            return result;
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _usersRepository.GetUserAsync(userId);
            var result = _mapper.Map<UserDto>(user);
            return result;
        }
    }
}
