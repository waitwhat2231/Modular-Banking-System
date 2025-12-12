using Common.SharedClasses.Pagination;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Commands;
using Modules.Users.Application.Commands.ConfirmEmail;
using Modules.Users.Application.Dtos;
using Modules.Users.Application.Queries.GetAll;
using Modules.Users.Application.Tokens.Commands.Refresh;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Endpoints.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(IMediator mediator, IUserContext userContext, IUsersService usersService) : ControllerBase
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

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<PagedEntity<MiniUserDto>>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var currentUserId = userContext.GetCurrentUser()!.Id;
            var result = await usersService.GetUserById(currentUserId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("token/refresh")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            var response = await mediator.Send(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}
