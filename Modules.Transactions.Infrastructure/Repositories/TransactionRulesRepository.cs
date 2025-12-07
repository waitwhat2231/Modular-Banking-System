using Common.SharedClasses.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Repositories
{
    class TransactionRulesRepository(TransactionsDbContext dbContext) : GenericRepository<TransactionApprovalRules>(dbContext), ITransactionRulesRepository
    {
        private readonly TransactionsDbContext _transactionsDbContext = dbContext;

        public async Task<TransactionApprovalRules> GetRuleFromHandlerName(string handlerName)
        {
            return await _transactionsDbContext.TransactionRules.FirstOrDefaultAsync(txrule => txrule.HandlerName == handlerName);
        }
    }
}
