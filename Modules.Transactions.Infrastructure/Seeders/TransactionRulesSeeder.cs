using Modules.Transactions.Domain.Entities;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Seeders;

internal class TransactionRulesSeeder(TransactionsDbContext transactionsDbContext) : ITransactionRulesSeeder
{
    public async Task Seed()
    {
        if (await transactionsDbContext.Database.CanConnectAsync())
        {
            if (!transactionsDbContext.TransactionRules.Any())
            {
                var transcationRules = GetTransactionRules();
                transactionsDbContext.TransactionRules.AddRange(transcationRules);
                await transactionsDbContext.SaveChangesAsync();
            }
        }
    }
    private static List<TransactionApprovalRules> GetTransactionRules()
    {
        List<TransactionApprovalRules> transactionApprovalRules = [
            new (){
                HandlerName = "AutoApprovalTransactionHandler",
                MinAmount = 0,
                MaxAmount = 1000,
            },
            new (){
                HandlerName = "ManagerApprovalHandler ",
                MinAmount = 1001,
                MaxAmount = 10000,
            },
            new (){
                HandlerName = "AdministratorApprovalHandler ",
                MinAmount = 10000,
                MaxAmount = null,
            },
            ];
        return transactionApprovalRules;
    }

}
