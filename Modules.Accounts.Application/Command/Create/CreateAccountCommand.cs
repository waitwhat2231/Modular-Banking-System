using MediatR;
using Modules.Accounts.Application.Dtos;
using Modules.Accounts.Domain.Enums;

namespace Modules.Accounts.Application.Command.Create;

public class CreateAccountCommand : IRequest<AccountDto>
{
    public string UserId { get; set; } = string.Empty;
    public int? ParentAccountId { get; set; }
    public AccountType Type { get; set; }
}
