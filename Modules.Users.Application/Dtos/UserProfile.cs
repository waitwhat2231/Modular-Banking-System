using AutoMapper;
using Common.SharedClasses.Dtos.Users;
using Modules.Users.Application.Commands;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Devices;

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
            CreateMap<Device, DeviceDto>();
        }
    }
}
