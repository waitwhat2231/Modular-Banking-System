using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.CommittingStrategies
{
    public interface ITransactionCommitStrategy
    {
        public Task CommitTransactionAsync(Transaction tx);
    }
}
