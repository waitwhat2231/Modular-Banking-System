using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Commands;
using Modules.Users.Application.Commands.ConfirmEmail;

namespace Modules.Users.Endpoints.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request)
        {
            var result = await mediator.Send(request);
            if (result.Any())
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("confirmEmail")]
        public async Task<ActionResult> CofirmUserEmail([FromBody] ConfirmEmailCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginUserCommand request)
        {

            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
