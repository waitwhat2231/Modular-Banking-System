using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using MediatR;

namespace Modules.Accounts.Application.Command.ChangeState;

public class ChangeAccountStateCommand : IRequest<AccountDto>
{
    public int AccountId { get; set; }
    public AccountState NewState { get; set; }
}
