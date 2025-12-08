using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;

namespace Common.SharedClasses.Services
{
    public interface IAccountService
    {
        public Task<AccountDto> GetAccountFromId(int accountId, bool tracking = true);
        Task UpdateAccount(int accountId, string? userId = null, int? parentAccountId = null, AccountState? accountState = null, AccountType? accountType = null, int? balance = null);
    }
}
