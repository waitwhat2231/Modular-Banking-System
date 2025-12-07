using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using MediatR;

namespace Modules.Accounts.Application.Command.Create;

public class CreateAccountCommand : IRequest<AccountDto>
{
    public string UserId { get; set; } = string.Empty;
    public int? ParentAccountId { get; set; }
    public AccountType Type { get; set; }
}
