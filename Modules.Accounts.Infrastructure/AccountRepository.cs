using Common.SharedClasses.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Repositories;
using Modules.Accounts.Infrastructure.Persistence;

namespace Modules.Accounts.Infrastructure;

public class AccountRepository(AccountsDbContext dbcontext) : GenericRepository<Account>(dbcontext), IAccountRepository
{
    public async Task<List<Account>> GetByUserIdAsync(string userId)
    {
        return await dbcontext.Accounts
            .Where(a => a.UserId == userId)
            .Include(a => a.Children)
            .ToListAsync();
    }

    public async Task<Account?> GetWithChildrenAsync(int accountId)
    {
        return await dbcontext.Accounts
            .Include(a => a.Children)
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }
}
