using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
}
