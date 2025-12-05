using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Strategies;

namespace Modules.Accounts.Infrastructure.Strategies;

public class SavingAccountStrategy : IAccountStrategy
{
    public void ApplyMonthlyInterest(Account account)
    {
        throw new NotImplementedException();
    }

    public int CalculateAvailableBalance(Account account)
    {
        throw new NotImplementedException();
    }

    public void ValidateDeposit(Account account, int amount)
    {
        throw new NotImplementedException();
    }

    public void ValidateWithdrawal(Account account, int amount)
    {
        throw new NotImplementedException();
    }
}
