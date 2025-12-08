using Common.SharedClasses.Repositories;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Domain.Repositories
{
    public interface ITransactionRulesRepository : IGenericRepository<TransactionApprovalRules>
    {
        public Task<TransactionApprovalRules> GetRuleFromHandlerName(string handlerName);
    }
}
