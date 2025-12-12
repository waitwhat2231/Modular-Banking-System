using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Pagination;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQuery(int pageNum, int pageSize, string userName = "") : IRequest<PagedEntity<AccountDto>>
{
    public int PageNum { get; set; } = pageNum;
    public int PageSize { get; set; } = pageSize;
    public string UserName { get; set; } = userName;
}
