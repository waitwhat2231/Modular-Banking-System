using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class WithdrawalStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction)
        {
            bool balanceDeducted = false;
            try
            {
                await accountService.UpdateAccount(accountId: (int)transaction.FromAccountId, balance: -1 * transaction.Amount);
                balanceDeducted = true;

                await transactionsRepository.AddAsync(transaction);
            }
            catch (Exception)
            {
                if (balanceDeducted)
                {
                    await accountService.UpdateAccount(accountId: (int)transaction.FromAccountId, balance: transaction.Amount);
                }
            }
        }
    }
}
