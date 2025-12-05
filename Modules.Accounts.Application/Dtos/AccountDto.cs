using Modules.Accounts.Domain.Enums;

namespace Modules.Accounts.Application.Dtos;

public class AccountDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int? ParentAccountId { get; set; }
    public AccountState State { get; set; }
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Balance { get; set; }

    public List<AccountDto> Children { get; set; } = [];
}
