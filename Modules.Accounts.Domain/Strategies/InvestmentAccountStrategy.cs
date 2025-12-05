using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Strategies;

namespace Modules.Accounts.Infrastructure.Strategies;

public class InvestmentAccountStrategy : IAccountStrategy
{
    public void ApplyMonthlyInterest(Account account)
    {
        throw new NotImplementedException();
    }

    public int CalculateAvailableBalance(Account account)
    {
        return account.Balance;
    }

    public void ValidateDeposit(Account account, int amount)
    {
        // variant لانو بتتغير حسب اذا ربح ولا خسارة
    }

    public void ValidateWithdrawal(Account account, int amount)
    {
        if (amount > account.Balance)
            throw new InvalidOperationException("Insufficient liquid funds.");
    }
}
