using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Queries.GetUsersAccounts;

public class GetUsersAccountsQueryHandler(IUserContext userContext, IMapper mapper,
    IAccountRepository accountRepository) : IRequestHandler<GetUsersAccountsQuery, List<AccountDto>>
{
    public async Task<List<AccountDto>> Handle(GetUsersAccountsQuery request, CancellationToken cancellationToken)
    {
        string currentUserId;
        var currentUser = userContext.GetCurrentUser();
        if (currentUser.isInRole("User") && request.UserId == null)
        {
            currentUserId = currentUser.Id;
        }
        else
            currentUserId = request.UserId;
        var accounts = await accountRepository.GetByUserIdAsync(currentUserId);
        var results = mapper.Map<List<AccountDto>>(accounts);
        return results;
    }
}
