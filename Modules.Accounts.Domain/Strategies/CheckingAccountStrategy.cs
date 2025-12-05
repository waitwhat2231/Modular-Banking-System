using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Strategies;

namespace Modules.Accounts.Infrastructure.Strategies;

public class CheckingAccountStrategy : IAccountStrategy
{
    public void ApplyMonthlyInterest(Account account)
    {
        // this kind of acc don't have interest
    }

    public int CalculateAvailableBalance(Account account)
    {
        return account.Balance + 500;
    }

    public void ValidateDeposit(Account account, int amount)
    {
        // this kind of acc don't have deposit
    }

    public void ValidateWithdrawal(Account account, int amount)
    {
        int overdraftLimit = -500;

        if (account.Balance - amount < overdraftLimit)
            throw new InvalidOperationException("Overdraft limit exceeded.");
    }
}
