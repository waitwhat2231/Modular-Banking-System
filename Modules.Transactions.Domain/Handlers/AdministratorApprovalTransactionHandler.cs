using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Domain.Handlers
{
    public class AdministratorApprovalTransactionHandler : TransactionHandler
    {
        public override Task HandleAsync(Transaction transaction)
        {
            if (transaction.Amount > 10000)
            {
                transaction.Status = Enums.EnumTransactionStatus.PendingAdmin;

            }
            return Task.CompletedTask;
        }
    }
}
