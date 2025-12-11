using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public string UserName { get; set; } = string.Empty;
}
