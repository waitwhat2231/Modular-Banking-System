using MediatR;
using Modules.Accounts.Application.Dtos;
using Modules.Accounts.Domain.Enums;

namespace Modules.Accounts.Application.Command.ChangeState;

public class ChangeAccountStateCommand : IRequest<AccountDto>
{
    public int AccountId { get; set; }
    public AccountState NewState { get; set; }
}
