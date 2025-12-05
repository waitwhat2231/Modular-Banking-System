using MediatR;
using Modules.Accounts.Application.Dtos;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
}
