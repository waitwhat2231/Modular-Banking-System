using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class WithdrawalStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction, string userId)
        {
            bool balanceDeducted = false;
            try
            {
                await accountService.UpdateAccount(accountId: (int)transaction.FromAccountId, balance: -1 * transaction.Amount);
                balanceDeducted = true;
                transaction.ApprovedAt = DateTime.UtcNow;
                transaction.Status = Domain.Enums.EnumTransactionStatus.Approved;
                transaction.ApprovedByUserId = userId;
                await transactionsRepository.SaveChangesAsync();
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
