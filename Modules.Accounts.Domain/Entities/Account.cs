using Modules.Accounts.Domain.Enums;

namespace Modules.Accounts.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int? ParentAccountId { get; set; }
    public Account? Parent { get; set; }
    public AccountState State { get; set; }
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Balance { get; set; }

    public List<Account> Children { get; set; } = [];
}
