namespace Modules.Users.Domain.Entities.Auth
{
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
        public string? UserId { get; set; }
    }
}
