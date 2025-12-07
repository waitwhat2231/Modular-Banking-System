using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Enums;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class TransferStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction, string userId)
        {
            var balanceDeducted = false;
            var balanceAdded = false;
            try
            {
                transaction.CreatedAt = DateTime.UtcNow;
                transaction.TransactionType = EnumTransactionType.Transfer;
                await accountService.UpdateAccount(accountId: (int)transaction.FromAccountId, balance: -1 * transaction.Amount);
                balanceDeducted = true;
                await accountService.UpdateAccount(accountId: (int)transaction.ToAccountId, balance: transaction.Amount);
                balanceAdded = true;
                transaction.Status = EnumTransactionStatus.Approved;
                transaction.ApprovedAt = DateTime.UtcNow;
                transaction.ApprovedByUserId = userId;
                await transactionsRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (balanceDeducted)
                {
                    await accountService.UpdateAccount(accountId: (int)transaction.FromAccountId, balance: transaction.Amount);
                }
                if (balanceAdded)
                {
                    await accountService.UpdateAccount(accountId: (int)transaction.ToAccountId, balance: -1 * transaction.Amount);
                }
            }
        }
    }
}
