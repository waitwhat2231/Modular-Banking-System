using Common.SharedClasses.Pagination;
using MediatR;
using Modules.Users.Application.Dtos;

namespace Modules.Users.Application.Queries.GetAll;

public class GetAllUsersQuery : IRequest<PagedEntity<MiniUserDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string UserName { get; set; } = string.Empty;
}
