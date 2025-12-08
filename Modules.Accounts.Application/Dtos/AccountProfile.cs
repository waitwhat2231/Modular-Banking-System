using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Modules.Accounts.Domain.Entities;

namespace Modules.Accounts.Application.Dtos;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>();
    }
}
