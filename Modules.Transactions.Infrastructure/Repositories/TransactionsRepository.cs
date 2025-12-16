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

        public async Task<PagedEntity<Transaction>> GetTrnasctionsPaged(int pageNum, int pageSize)
        {
            var transactions = await _transactiondbcontext.Transactions
           .Skip((pageNum - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            var result = new PagedEntity<Transaction>()
            {
                Items = transactions,
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalItems = _transactiondbcontext.Transactions.Count()

            };
            return result;
        }
    }
}
