using Common.SharedClasses.Services;
using MediatR;
using Modules.Users.Domain.Entities.Auth;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.Tokens.Commands.Refresh;

public class RefreshTokenCommandHandler(IUserContext userContext, ITokenRepository tokenRepository) : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string user;
            if (userContext.GetCurrentUser() != null)
            {
                user = userContext.GetCurrentUser().Id.ToString();
            }
            else
            {
                var token = userContext.GetAccessToken();
                if (token == null)
                {
                    throw new InvalidOperationException();
                }
                user = tokenRepository.ReadInvalidToken(token);
            }
            var req = new RefreshTokenRequest
            {
                UserId = user.ToString(),
                RefreshToken = request.RefreshToken
            };
            var response = await tokenRepository.VerifyRefreshToken(req);
            return response;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
