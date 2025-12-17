using Common.SharedClasses.Enums;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Repositories
{
    class TransactionsRepository(TransactionsDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionsRepository
    {
        private readonly TransactionsDbContext _transactiondbcontext = dbContext;

        public async Task<PagedEntity<Transaction>> GetTransactionsPaged(int pageNum, int pageSize, List<int>? accountIds, DateTime? from, DateTime? to, EnumTransactionType? type, EnumTransactionStatus? status)
        {
            var query = _transactiondbcontext.Transactions.AsQueryable();
            if (accountIds != null && accountIds.Any())
            {
                query = query.Where(t => (t.FromAccountId != null && accountIds.Contains((int)t.FromAccountId)) ||
                (t.ToAccountId != null && accountIds.Contains((int)t.ToAccountId)));
            }
            if (from != null)
            {
                query = query.Where(t => t.CreatedAt >= from);
            }
            if (to != null)
            {
                query = query.Where(t => t.CreatedAt <= to);
            }
            if (type != null)
            {
                query = query.Where(t => t.TransactionType == type);
            }
            if (status != null)
            {
                query = query.Where(t => t.Status == status);
            }
            var transactions = await query
           .Skip((pageNum - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            var result = new PagedEntity<Transaction>()
            {
                Items = transactions,
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalItems = query.Count()

            };
            return result;
        }
    }
}
