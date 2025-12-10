using Common.SharedClasses.Services;
using MediatR;
using Modules.Users.Application.Dtos;

namespace Modules.Users.Application.Queries.GetAll;

public class GetAllUsersQueryHandler(IUsersService usersService) : IRequestHandler<GetAllUsersQuery, List<MiniUserDto>>
{
    public async Task<List<MiniUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await usersService.GetAllUsersAsync(request.Page, request.PageSize, request.UserName);
        return users;
    }
}
