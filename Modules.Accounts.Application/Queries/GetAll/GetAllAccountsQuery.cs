using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQuery(int pageNum, int pageSize, string userName = "") : IRequest<List<AccountDto>>
{
    public int PageNum { get; set; } = pageNum;
    public int PageSize { get; set; } = pageSize;
    public string UserName { get; set; } = userName;
}
