using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Queries.GetById;

public class GetAccountByIdQueryHandler(IAccountRepository accountRepository,
    IMapper mapper, IUsersService usersService) : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetWithChildrenAsync(request.AccountId);

        if (account is null)
            throw new InvalidOperationException("Account not found.");

        var user = await usersService.GetUserById(account.UserId);

        if (user is null)
            throw new NotFoundException("User", account.UserId);

        var accountDto = mapper.Map<AccountDto>(account);
        accountDto.UserName = user.UserName ?? "";

        return accountDto;
    }
}
