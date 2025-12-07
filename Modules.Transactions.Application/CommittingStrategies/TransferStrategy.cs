
using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.CommittingStrategies
{
    class TransferStrategy(IAccountService accountService, ITransactionsRepository transactionsRepository) : ITransactionCommitStrategy
    {
        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
