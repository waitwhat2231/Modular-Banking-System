using MediatR;
using Modules.Users.Domain.Entities.Auth;

namespace Modules.Users.Application.Tokens.Commands.Refresh;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string? RefreshToken { get; set; }
}
