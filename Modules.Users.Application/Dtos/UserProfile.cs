using AutoMapper;
using Modules.Users.Application.Commands;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<MiniUserDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<User, UserDto>();
        }
    }
}
