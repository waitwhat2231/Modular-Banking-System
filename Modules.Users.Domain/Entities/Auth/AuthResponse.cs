namespace Modules.Users.Domain.Entities.Auth;

public class AuthResponse
{
    public string? Token { get; set; }
    public string? Username { get; set; }
    public int? Expires { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }

}
