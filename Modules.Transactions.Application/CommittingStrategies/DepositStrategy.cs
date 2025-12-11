using Common.SharedClasses.Enums;
using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class DepositStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction, string userId)
        {
            bool balanceAdded = false;
            try
            {
                await accountService.UpdateAccount(accountId: (int)transaction.ToAccountId, balance: transaction.Amount);
                balanceAdded = true;
                transaction.Status = EnumTransactionStatus.Approved;
                transaction.ApprovedAt = DateTime.UtcNow;
                transaction.ApprovedByUserId = userId;
                await transactionsRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (balanceAdded)
                {
                    await accountService.UpdateAccount(accountId: (int)transaction.ToAccountId, balance: -1 * transaction.Amount);
                }
            }
        }
    }
}
