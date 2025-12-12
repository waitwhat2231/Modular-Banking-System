using Common.SharedClasses.Pagination;
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
    public async Task<PagedEntity<Account>> GetAccountsFiltered(List<string> userIds, int pageNum, int pageSize)
    {
        var accounts = await dbcontext.Accounts.Where(
            a => userIds.Contains(a.UserId))
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var result = new PagedEntity<Account>()
        {
            Items = accounts,
            PageNumber = pageNum,
            PageSize = pageSize,
            TotalItems = dbcontext.Accounts.Count()

        };
        return result;
    }

}
