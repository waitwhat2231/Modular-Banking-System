using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Services;
using Modules.Accounts.Domain.JobRelatedServices;
using Modules.Accounts.Domain.Repositories;
using System.Transactions;

namespace Modules.Accounts.Infrastructure.JobRelatedServices;

public class InterestHandler(ITransactionService transactionService, IAccountRepository accountRepository) : IInterestHandler
{
    public async Task ApplyInterestToAllAccounts()
    {
        var accounts = await accountRepository.GetAllAsync();
        List<AddTransactionDto> transactions = [];
        foreach (var account in accounts)
        {
            account.Balance += account.AccuredInterest;
            transactions.Add(new AddTransactionDto()
            {
                ToAccountId = account.Id,
                Amount = account.AccuredInterest,
                ApprovedByUserId = "System",
                ApprovedAt = DateTime.UtcNow,
                Status = Common.SharedClasses.Enums.EnumTransactionStatus.Approved,
                Type = Common.SharedClasses.Enums.EnumTransactionType.Routine,
            });
        }
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await accountRepository.SaveChangesAsync();
            await transactionService.AddTransactionBatch(transactions);
            scope.Complete();
        }
    }
    public async Task CalculatedailyAccuredInterest()
    {
        var accounts = await accountRepository.GetAllAsync();
        foreach (var account in accounts)
        {
            var dailyInterest = (account.Balance * 0.05) / 365;
            account.AccuredInterest += (int)dailyInterest;
        }
        await accountRepository.SaveChangesAsync();
    }
}
