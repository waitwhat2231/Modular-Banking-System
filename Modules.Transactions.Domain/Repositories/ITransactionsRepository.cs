using Common.SharedClasses.Enums;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Repositories;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Domain.Repositories
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        public Task<PagedEntity<Transaction>> GetTransactionsPaged(int pageNum, int pageSize, List<int>? accountIds, DateTime? from, DateTime? to, EnumTransactionType? type, EnumTransactionStatus? status);
    }
}
