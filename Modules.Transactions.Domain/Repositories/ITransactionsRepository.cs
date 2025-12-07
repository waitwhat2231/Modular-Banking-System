using Common.SharedClasses.Repositories;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Domain.Repositories
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
    }
}
