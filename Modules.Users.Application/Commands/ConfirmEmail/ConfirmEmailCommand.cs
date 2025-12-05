using MediatR;

namespace Modules.Users.Application.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
