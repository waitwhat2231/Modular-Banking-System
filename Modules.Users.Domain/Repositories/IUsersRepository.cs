using Common.SharedClasses.Pagination;
using Common.SharedClasses.Repositories;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Auth;
namespace Modules.Users.Domain.Repositories
{

    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<bool> CheckPassword(string userId, string password);
        Task<User> GetUserAsync(string id);
        Task<User> FindUserByEmail(string email);
        Task<User> FindUserByUserName(string userName);
        Task<User> GetUserDetails(string? id);
        Task<User> GetUserWithDevicesAsync(string id);
        //Task<List<User>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName);
        Task<AuthResponse>? LoginUser(string email, string password, string deviceToken);
        Task<int> NewUsersAfterMonth(int month, string roleId, int year);
        Task<int> NumberOfUsersInRole(string roleId);
        Task<IEnumerable<IdentityError>> Register(User owner, string password, string role);
        Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password);
        Task UpdateUser(User user);
        Task<bool> UserInRoleAsync(string id, string roleName);
        Task<List<User>> GetAssistants(string? sortByRating);
        public Task DeleteAccount(string userId);
        Task<(User user, IdentityResult result)> UpdateUserAsync(User user);
        Task<List<(User user, string? roleName)>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName);
        Task<IdentityResult> UpdatePassword(User user, string oldPassword, string newPassword);
        Task SendEmail(string userEmail, string code);
        Task<IEnumerable<IdentityError>> ResetPassword(string token, string newPassword);
        Task<(bool IsValid, string Token)> VerifyForgotPasswordOtp(string code);
        Task<IdentityResult> UpdateSecurityStampAsync(string userId);
        Task DeleteUser(User user);
        Task<User?> FindUserById(string userId);
        Task<User?> FindUserByIdOptionalTracking(string userId, bool asNoTracking = false);
        Task<AuthResponse>? LoginUserWithoutDevice(string email, string password);
        Task<bool> ConfirmEmailAsync(string email, string code);
        Task<PagedEntity<User>> GetAllPaginatedAsync(int page, int pageSize, string userName);
        Task<List<User>> GetAllUsersNotPaginated(string userName);
    }
}
