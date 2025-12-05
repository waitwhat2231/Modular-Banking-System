using MediatR;
using Modules.Accounts.Application.Dtos;
using Modules.Accounts.Domain.Enums;

namespace Modules.Accounts.Application.Command.Update;

public class UpdateAccountCommand : IRequest<AccountDto>
{
    public int AccountId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int? ParentAccountId { get; set; }
    public AccountType Type { get; set; }
}
