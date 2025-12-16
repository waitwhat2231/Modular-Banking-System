using Common.SharedClasses.Enums;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Handlers;

public class ManagerApprovalHandler(ITransactionRulesRepository transactionRulesRepository) : TransactionHandler
{
    public override async Task HandleAsync(Transaction transaction)
    {
        string name = GetType().Name;
        var transactionRule = await transactionRulesRepository.GetRuleFromHandlerName(name);
        if (transaction.Amount > transactionRule.MinAmount && transaction.Amount <= transactionRule.MaxAmount)
        {
            transaction.Status = EnumTransactionStatus.PendingManager;
            return;
        }

        if (_next != null)
            await _next.HandleAsync(transaction);
    }
}
