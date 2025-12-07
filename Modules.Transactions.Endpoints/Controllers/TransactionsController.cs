using Common.SharedClasses.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Transactions.Application.Commands;
using Modules.Transactions.Application.Commands.ChangeStatus;

namespace Modules.Transactions.Endpoints.Controllers
{
    [ApiController]
    [Route("/api/account/")]
    public class TransactionsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("{accountId:int}/Withdraw")]

        public async Task<ActionResult> Withdraw([FromRoute] int aaccountId, [FromBody] WithdrawalCommand command)
        {
            command.AccountId = aaccountId;
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{accountId:int}/Deposit")]

        public async Task<ActionResult> Deposit([FromRoute] int aaccountId, [FromBody] DepositCommand command)
        {
            command.AccountId = aaccountId;
            await mediator.Send(command);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        [Route("{accountId:int}/Transfer")]

        public async Task<ActionResult> Withdraw([FromRoute] int aaccountId, [FromBody] TransferCommand command)
        {
            command.FromAccountId = aaccountId;
            await mediator.Send(command);
            return Ok();
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

    }
}
