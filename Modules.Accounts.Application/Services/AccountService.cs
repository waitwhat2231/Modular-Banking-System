using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Services;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Services
{
    public class AccountService(IAccountRepository accountRepository, IMapper mapper) : IAccountService
    {
        public async Task<AccountDto> GetAccountFromId(int accountId, bool tracking = true)
        {
            var account = await accountRepository.FindByIdAsync(accountId);
            var result = mapper.Map<AccountDto>(account);
            return result;
        }
        public async Task UpdateAccount(int accountId, string? userId = null, int? parentAccountId = null, AccountState? accountState = null,
            AccountType? accountType = null, int? balance = null)
        {
            var account = await accountRepository.FindByIdAsync(accountId);
            if (userId != null)
            {
                account.UserId = userId;
            }
            if (parentAccountId != null)
            {
                account.ParentAccountId = parentAccountId;
            }
            if (accountState != null)
            {
                account.State = (AccountState)accountState;
            }
            if (accountType != null)
            {
                account.Type = (AccountType)accountType;
            }
            if (balance != null)
            {
                account.Balance += (int)balance;
            }
            await accountRepository.SaveChangesAsync();

        }
    }
}
