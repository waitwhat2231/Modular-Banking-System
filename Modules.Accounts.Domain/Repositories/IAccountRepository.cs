using Common.SharedClasses.Repositories;
using Modules.Accounts.Domain.Entities;

namespace Modules.Accounts.Domain.Repositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    public Task<Account?> GetWithChildrenAsync(int accountId);
    public Task<List<Account>> GetByUserIdAsync(string userId);
    Task<List<Account>> GetAccountsFiltered(List<string> userIds, int pageNum, int pageSize);
}
