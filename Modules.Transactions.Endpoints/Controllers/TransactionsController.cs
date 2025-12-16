using Common.SharedClasses.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Transactions.Application.Commands;
using Modules.Transactions.Application.Commands.ChangeStatus;
using Modules.Transactions.Application.Queries.Rules;

namespace Modules.Transactions.Endpoints.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TransactionsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = $"{nameof(EnumRoleNames.Manager)},{nameof(EnumRoleNames.Administrator)}")]
        [Route("{accountId:int}/Withdraw")]

        public async Task<ActionResult> Withdraw([FromRoute] int accountId, [FromBody] WithdrawalCommand command)
        {
            command.AccountId = accountId;
            var res = await mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(EnumRoleNames.Manager)},{nameof(EnumRoleNames.Administrator)}")]
        [Route("{accountId:int}/Deposit")]

        public async Task<ActionResult> Deposit([FromRoute] int accountId, [FromBody] DepositCommand command)
        {
            command.AccountId = accountId;
            var res = await mediator.Send(command);
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("{fromAccountId:int}/Transfer")]

        public async Task<ActionResult> Withdraw([FromRoute] int fromAccountId, [FromBody] TransferCommand command)
        {
            command.FromAccountId = fromAccountId;
            var res = await mediator.Send(command);
            return Ok(res);
        }
        [HttpPost]
        [Authorize(Roles = $"{nameof(EnumRoleNames.Manager)},{nameof(EnumRoleNames.Administrator)}")]
        [Route("{transactionId:int}/ChangeStatus")]

        public async Task<ActionResult> ChangeStatus([FromRoute] int transactionId, [FromBody] ChangeTransactionStatusCommand command)
        {
            command.TransactionId = transactionId;
            await mediator.Send(command);
            return Ok();
        }
        [HttpGet("TransactionSatusEnum")]
        public IActionResult GetTransactionStatusEnum()
        {
            var result = EnumHelper.ToEnumDtoList<EnumTransactionStatus>();
            return Ok(result);
        }
        [HttpGet("TransactionTypeEnum")]
        public IActionResult GetTransactionTypeEnum()
        {
            var result = EnumHelper.ToEnumDtoList<EnumTransactionType>();
            return Ok(result);
        }
        [HttpGet("Rules")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetTransactionRules()
        {
            await mediator.Send(new GetTransactionRulesQuery());
            return Ok();
        }


    }
}
