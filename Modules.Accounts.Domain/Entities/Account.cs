using Common.SharedClasses.Enums;

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

    private Account() { }

    public Account(string userId, AccountType type)
    {
        UserId = userId;
        Type = type;
        CreatedAt = DateTime.UtcNow;
        State = AccountState.Active;
    }

    public Account(string userId, AccountType type, int? parentAccountId)
    {
        UserId = userId;
        Type = type;
        ParentAccountId = parentAccountId;
        CreatedAt = DateTime.UtcNow;
        State = AccountState.Active;
    }

    public void AddChild(Account child)
    {
        if (State == AccountState.Closed)
            throw new InvalidOperationException("Cannot add sub-accounts to a closed account.");

        child.Parent = this;
        child.ParentAccountId = this.Id;

        Children.Add(child);
    }

    public void Close()
    {
        State = AccountState.Closed;

        foreach (var child in Children)
            child.Close();
    }

    public void Freeze()
    {
        State = AccountState.Frozen;

        foreach (var child in Children)
            child.Freeze();
    }

    public void Activate()
    {
        State = AccountState.Active;

        foreach (var child in Children)
            child.Activate();
    }

    public void Suspend()
    {
        State = AccountState.Suspended;

        foreach (var child in Children)
            child.Suspend();
    }

    public int GetTotalBalance()
    {
        int total = Balance;

        foreach (var child in Children)
            total += child.GetTotalBalance();

        return total;
    }
}
