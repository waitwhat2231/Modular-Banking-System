using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetUsersAccounts;

public class GetUsersAccountsQuery(string? userId = null) : IRequest<List<AccountDto>>
{
    public string? UserId { get; set; } = userId;
}
