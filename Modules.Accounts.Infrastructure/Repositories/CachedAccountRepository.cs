using Common.SharedClasses.Pagination;
using Microsoft.Extensions.Caching.Memory;
using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Infrastructure.Repositories
{
    public class CachedAccountRepository : IAccountRepository
    {
        private readonly AccountRepository _decorated;
        private readonly IMemoryCache _memoryCache;
        public CachedAccountRepository(AccountRepository repository, IMemoryCache memoryCache)
        {
            _decorated = repository;
            _memoryCache = memoryCache;
        }
        public async Task<Account> AddAsync(Account entity)
        {
            await _decorated.AddAsync(entity);
            return entity;
        }

        public async Task<List<Account>> AddBatch(List<Account> entityList)
        {
            await _decorated.AddBatch(entityList);
            return entityList;
        }

        public async Task<Account?> FindByIdAsync(int id)
        {
            return await _decorated.FindByIdAsync(id);
        }

        public Task<PagedEntity<Account>> GetAccountsFiltered(List<string> userIds, int pageNum, int pageSize)
        {
            return _decorated.GetAccountsFiltered(userIds, pageNum, pageSize);
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            return _decorated.GetAllAsync();
        }

        public Task<Account?> GetByIdOptionalTracking(int id, bool tracking = true)
        {
            return _decorated.GetByIdOptionalTracking(id, tracking);
        }

        public async Task<List<Account>> GetByUserIdAsync(string userId)
        {
            string key = $"AccountFromUserId-{userId}";
            return await _memoryCache.GetOrCreateAsync(
          key,
          entry =>
          {
              entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

              return _decorated.GetByUserIdAsync(userId);
          });
        }

        public Task<IEnumerable<Account>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return _decorated.GetPagedResponseAsync(pageNumber, pageSize);
        }

        public async Task<Account?> GetWithChildrenAsync(int accountId)
        {
            string key = $"Account-{accountId}";
            return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return _decorated.GetWithChildrenAsync(accountId);
                }
                );
        }

        public async Task HardDeleteAsync(Account entity)
        {
            await _decorated.HardDeleteAsync(entity);
        }

        public async Task SaveChangesAsync()
        {

            await _decorated.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Account entity)
        {
            await _decorated.SoftDeleteAsync(entity);
        }

        public async Task UpdateAccount(Account account)
        {
            string accountKey = $"Account-{account.Id}";
            string accountFromUserId = $"AccountFromUserId-{account.UserId}";
            if (_memoryCache.TryGetValue(accountKey, out _))
            {
                _memoryCache.Remove(accountKey);
            }
            if (_memoryCache.TryGetValue(accountFromUserId, out _))
            {
                _memoryCache.Remove(accountFromUserId);
            }
            await _decorated.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account entity)
        {
            string accountKey = $"Account-{entity.Id}";
            string accountFromUserId = $"AccountFromUserId-{entity.UserId}";
            if (_memoryCache.TryGetValue(accountKey, out _))
            {
                _memoryCache.Remove(accountKey);
            }
            if (_memoryCache.TryGetValue(accountFromUserId, out _))
            {
                _memoryCache.Remove(accountFromUserId);
            }
            await _decorated.UpdateAsync(entity);
        }
    }
}
