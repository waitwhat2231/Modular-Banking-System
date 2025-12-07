using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Handlers;

public class AutoApprovalTransactionHandler : TransactionHandler
{
    private readonly ITransactionRulesRepository _txrulesRepository;
    public AutoApprovalTransactionHandler(ITransactionRulesRepository transactionRulesRepository)
    {
        _txrulesRepository = transactionRulesRepository;
    }
    public override async Task HandleAsync(Transaction tx)
    {
        var transactionRule = await _txrulesRepository.GetRuleFromHandlerName(GetType().Name);
        if (tx.Amount <= transactionRule.MaxAmount && transactionRule.IsActive)
        {
            tx.Status = Domain.Enums.EnumTransactionStatus.Approved;
            tx.ApprovedByUserId = "System";
            tx.ApprovedAt = DateTime.UtcNow;
            return;
        }

        if (_next != null)
            await _next.HandleAsync(tx);
    }

}
