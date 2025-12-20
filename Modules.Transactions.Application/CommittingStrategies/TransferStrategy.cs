using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using Modules.Transactions.Application.Events;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class TransferStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository, IDomainEventDispatcher domainEventDispatcher) : ITransactionCommitStrategy
    {
        public async Task CommitTransactionAsync(Transaction transaction, string userId)
        {
            var balanceDeducted = false;
            var balanceAdded = false;
            var toAccount = await accountService.GetAccountFromId((int)transaction.ToAccountId, false);
            try
            {
                var fromAccount = await accountService.GetAccountFromId((int)transaction.FromAccountId, false);
                if (fromAccount.Balance < transaction.Amount)
                {
                    throw new NoBalanceException((int)transaction.FromAccountId);
                }
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
                transaction.Complete(fromAccount.UserId);
                transaction.Complete(toAccount.UserId);
                await domainEventDispatcher.DispatchAsync(transaction.DomainEvents);
                transaction.ClearDomainEvents();
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
