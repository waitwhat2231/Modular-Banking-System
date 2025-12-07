using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper) : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetAllAsync();

        var result = mapper.Map<List<AccountDto>>(accounts);

        return result;
    }
}
