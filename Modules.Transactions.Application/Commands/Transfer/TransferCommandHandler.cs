using AutoMapper;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Transactions.Application.Commands.Deposit;
using Modules.Transactions.Application.Handlers;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Enums;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Commands.Transfer
{
    class TransferCommandHandler(
         ITransactionsRepository transactionsRepository, IAccountService accountService, ILogger<DepositCommandHandler> logger,
    IMapper mapper, TransactionApprovalChain approvalHandler, IUserContext userContext
        ) : IRequestHandler<TransferCommand>
    {
        public async Task Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            bool balanceDeducted = false;
            bool balanceAdded = false;
            logger.LogInformation("Beginning Withdrawal Transaction");

            try
            {
                var sender = userContext.GetCurrentUser();
                var transaction = mapper.Map<Transaction>(request);
                transaction.CreatedAt = DateTime.UtcNow;
                transaction.TransactionType = EnumTransactionType.Transfer;
                await approvalHandler.ExecuteAsync(transaction);
                if (transaction.Status == EnumTransactionStatus.Approved)
                {
                    await accountService.UpdateAccount(accountId: request.FromAccountId, balance: -1 * transaction.Amount);
                    balanceDeducted = true;
                    await accountService.UpdateAccount(accountId: request.ToAccountId, balance: transaction.Amount);
                    balanceAdded = true;
                }
                await transactionsRepository.AddAsync(transaction);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                if (balanceDeducted)
                {
                    await accountService.UpdateAccount(accountId: request.FromAccountId, balance: request.Amount);
                }
                if (balanceAdded)
                {
                    await accountService.UpdateAccount(accountId: request.ToAccountId, balance: -1 * request.Amount);
                }
            }
        }
    }
}
