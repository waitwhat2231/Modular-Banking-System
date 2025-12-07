using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetUsersAccounts;

public class GetUsersAccountsQuery : IRequest<List<AccountDto>>
{
}
