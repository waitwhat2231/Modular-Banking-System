using MediatR;
using Modules.Accounts.Application.Dtos;

namespace Modules.Accounts.Application.Queries.GetUsersAccounts;

public class GetUsersAccountsQuery : IRequest<List<AccountDto>>
{
}
