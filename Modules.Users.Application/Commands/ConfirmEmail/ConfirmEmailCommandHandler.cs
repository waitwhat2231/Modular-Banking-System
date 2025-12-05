using Common.SharedClasses.Exceptions;
using MediatR;
using Modules.Users.Application.Commands.ConfirmEmail;
using Modules.Users.Domain.Repositories;

namespace Template.Application.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(IUsersRepository accountRepository) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var emailConfirmed = await accountRepository.ConfirmEmailAsync(request.Email, request.Code);
        if (!emailConfirmed)
        {
            throw new BadRequestException("Email or OTP is wrong");
        }
    }
}
