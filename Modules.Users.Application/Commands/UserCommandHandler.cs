using AutoMapper;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Auth;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.Commands;

public class RegisterUserCommandHandler(IMapper mapper,
        IUserContext userContext,
        ILogger<RegisterUserCommandHandler> logger,
        IUsersRepository accountRepository
        ) : IRequestHandler<RegisterUserCommand, IEnumerable<IdentityError>>
{
    public async Task<IEnumerable<IdentityError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var currentUser = userContext.GetCurrentUser();
        var currentUserRole = currentUser != null ? currentUser.Roles.FirstOrDefault().ToUpper() : "NOROLE";
        if (request.Role.ToUpper() != nameof(EnumRoleNames.User).ToUpper()
            && currentUserRole != nameof(EnumRoleNames.Administrator).ToUpper()
            )
        {
            throw new ForbiddenException("Registering an AdminAccount");
        }
        user.UserName = request.UserName;
        var errors = await accountRepository.Register(user, request.Password, request.Role);
        return errors;
    }
    public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
            IUsersRepository accountRepository) : IRequestHandler<LoginUserCommand, AuthResponse?>
    {
        public async Task<AuthResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("looking for user with email: {Email}", request.Email);
            var tokenResponse = await accountRepository.LoginUser(request.Email, request.Password, request.DeviceToken);
            if (tokenResponse != null)
            {
                return tokenResponse;
            }
            return null;
        }
    }
}
