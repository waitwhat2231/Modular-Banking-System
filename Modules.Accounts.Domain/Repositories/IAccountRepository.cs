using Common.SharedClasses.Repositories;
using Modules.Accounts.Domain.Entities;

namespace Modules.Accounts.Domain.Repositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    public Task<Account?> GetWithChildrenAsync(int accountId);
}
