using MediatR;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;

namespace Modules.Users.Application.Commands;
public class RegisterUserCommand : IRequest<IEnumerable<IdentityError>>
{
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
}

public class LoginUserCommand : IRequest<AuthResponse?>
{

    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? DeviceToken { get; set; }
}


