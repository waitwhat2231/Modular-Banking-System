using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Strategies;

namespace Modules.Accounts.Infrastructure.Strategies;

public class LoanAccountStrategy : IAccountStrategy
{
    public void ApplyMonthlyInterest(Account account)
    {
        //فائدة عالقرض
        account.Balance -= (int)(account.Balance * -0.05);
    }

    public int CalculateAvailableBalance(Account account)
    {
        return 0; // cant withdraw
    }

    public void ValidateDeposit(Account account, int amount)
    {
        // nth for now, because it can always be add on (*loan* account)
    }

    public void ValidateWithdrawal(Account account, int amount)
    {
        throw new InvalidOperationException("Loan accounts do not allow withdrawals.");
    }
}
