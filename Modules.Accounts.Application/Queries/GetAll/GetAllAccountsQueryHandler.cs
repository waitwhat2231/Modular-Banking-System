using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Queries.GetAll;

public class GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper, IUsersService usersService) : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var users = await usersService.GetAllUsersNoPagination(request.UserName);
        var userIds = users.Select(u => u.Id).ToList();
        var accounts = await accountRepository.GetAccountsFiltered(userIds, request.PageNum, request.PageSize);

        var result = accounts.Select(a => new AccountDto()
        {
            Id = a.Id,
            Balance = a.Balance,
            UserId = a.UserId,
            UserName = users.First(u => u.Id == a.UserId).UserName,
            CreatedAt = a.CreatedAt,
            ParentAccountId = a.ParentAccountId,
            State = a.State,
            Type = a.Type,
        }).ToList();
        return result;
    }
}
