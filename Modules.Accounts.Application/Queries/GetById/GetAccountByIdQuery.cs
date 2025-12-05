using MediatR;
using Modules.Accounts.Application.Dtos;

namespace Modules.Accounts.Application.Queries.GetById;

public class GetAccountByIdQuery(int accountId) : IRequest<AccountDto>
{
    public int AccountId { get; } = accountId;
}
