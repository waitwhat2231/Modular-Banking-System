using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class DepositStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction)
        {
            bool balanceAdded = false;
            try
            {

            }
            catch (Exception ex)
            {
                if (balanceAdded)
                {
                    await accountService.UpdateAccount(accountId: (int)transaction.ToAccountId, balance: transaction.Amount);
                }
            }
        }
    }
}
