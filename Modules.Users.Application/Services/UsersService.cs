using AutoMapper;
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

        public async Task<List<MiniUserDto>> GetAllUsersAsync(int page, int pageSize, string userName)
        {
            var users = await _usersRepository.GetAllPaginatedAsync(page, pageSize, userName);
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
