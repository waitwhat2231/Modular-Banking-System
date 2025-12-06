using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Handlers;

public class AutoApprovalTransactionHandler : TransactionHandler
{
    public override async Task HandleAsync(Transaction tx)
    {
        if (tx.Amount <= 1000)
        {
            tx.Status = Domain.Enums.EnumTransactionStatus.Approved;
            tx.ApprovedAt = DateTime.UtcNow;
            return;
        }

        if (_next != null)
            await _next.HandleAsync(tx);
    }

}
