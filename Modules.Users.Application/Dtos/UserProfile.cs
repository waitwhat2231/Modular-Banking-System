using AutoMapper;
using Modules.Users.Application.Commands;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
