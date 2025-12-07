using Common.SharedClasses.Dtos.Accounts;
using MediatR;

namespace Modules.Accounts.Application.Queries.GetById;

public class GetAccountByIdQuery(int accountId) : IRequest<AccountDto>
{
    public int AccountId { get; } = accountId;
}
