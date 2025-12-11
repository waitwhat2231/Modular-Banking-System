using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Accounts.Application.Command.ChangeState;
using Modules.Accounts.Application.Command.Create;
using Modules.Accounts.Application.Command.Update;
using Modules.Accounts.Application.Queries.GetAll;
using Modules.Accounts.Application.Queries.GetById;
using Modules.Accounts.Application.Queries.GetUsersAccounts;

namespace Modules.Accounts.Endpoints.Controller;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}/ChangeState")]
    public async Task<IActionResult> ChangeState(int id, [FromBody] ChangeAccountStateCommand command)
    {
        command.AccountId = id;
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}/Update")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountCommand command)
    {
        command.AccountId = id;
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNum, int pageSize, string userName = "")
    {
        var result = await mediator.Send(new GetAllAccountsQuery(pageNum, pageSize, userName));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetAccountByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("GetByUserId")]
    public async Task<IActionResult> GetByUserId(int id)
    {
        var result = await mediator.Send(new GetUsersAccountsQuery());
        return Ok(result);
    }
}
