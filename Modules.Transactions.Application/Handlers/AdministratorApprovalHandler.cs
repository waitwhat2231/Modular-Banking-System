using Common.SharedClasses.Enums;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;


namespace Modules.Transactions.Application.Handlers;

public class AdministratorApprovalHandler(ITransactionRulesRepository transactionRulesRepository) : TransactionHandler
{
    public async override Task HandleAsync(Transaction transaction)
    {
        string name = GetType().Name;
        var transactionRule = await transactionRulesRepository.GetRuleFromHandlerName(name);
        if (transaction.Amount > transactionRule.MinAmount && transaction.Amount <= transactionRule.MaxAmount)
        {
            transaction.Status = EnumTransactionStatus.PendingAdmin;

        }
        return;
    }
}
