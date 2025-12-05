using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Auth;

namespace Modules.Users.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<string> CreateRefreshToken();
        Task<AuthResponse?> GenerateToken(string UserIdentifier);
        string ReadInvalidToken(string token);
        Task TokenDelete(User user);
        Task<AuthResponse?> VerifyRefreshToken(RefreshTokenRequest request);
    }
}
