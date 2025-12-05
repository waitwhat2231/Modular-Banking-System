using Modules.Accounts.Domain.Entities;

namespace Modules.Accounts.Domain.Strategies;

public interface IAccountStrategy
{
    void ValidateDeposit(Account account, int amount); // this can work for all
    void ValidateWithdrawal(Account account, int amount);
    int CalculateAvailableBalance(Account account);
    void ApplyMonthlyInterest(Account account);
}
