using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Queries.GetById;

public class GetAccountByIdQueryHandler(IAccountRepository accountRepository,
    IMapper mapper) : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetWithChildrenAsync(request.AccountId);

        if (account == null)
            throw new InvalidOperationException("Account not found.");

        return mapper.Map<AccountDto>(account);
    }
}
