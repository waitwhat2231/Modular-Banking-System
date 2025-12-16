using Common.SharedClasses.Enums;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Handlers;

public class AutoApprovalHandler : TransactionHandler
{
    private readonly ITransactionRulesRepository _txrulesRepository;
    public AutoApprovalHandler(ITransactionRulesRepository transactionRulesRepository)
    {
        _txrulesRepository = transactionRulesRepository;
    }
    public override async Task HandleAsync(Transaction tx)
    {
        string name = GetType().Name;
        var transactionRule = await _txrulesRepository.GetRuleFromHandlerName(name);
        if (tx.Amount <= transactionRule.MaxAmount && transactionRule.IsActive)
        {
            tx.Status = EnumTransactionStatus.Approved;
            tx.ApprovedByUserId = "System";
            tx.ApprovedAt = DateTime.UtcNow;
            return;
        }

        if (_next != null)
            await _next.HandleAsync(tx);
    }

}
