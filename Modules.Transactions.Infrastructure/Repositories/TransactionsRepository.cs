using Common.SharedClasses.Repositories;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Repositories
{
    class TransactionsRepository(TransactionsDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionsRepository
    {
        private readonly TransactionsDbContext _transactiondbcontext = dbContext;
    }
}
