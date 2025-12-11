using Common.SharedClasses.Enums;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Handlers;

public class ManagerApprovalHandler : TransactionHandler
{
    public override async Task HandleAsync(Transaction transaction)
    {
        if (transaction.Amount > 1000 && transaction.Amount <= 10000)
        {
            transaction.Status = EnumTransactionStatus.PendingManager;
            return;
        }

        if (_next != null)
            await _next.HandleAsync(transaction);
    }
}
