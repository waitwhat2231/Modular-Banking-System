using AutoMapper;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Transactions.Application.Commands.Deposit;
using Modules.Transactions.Application.Handlers;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Enums;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Commands.Withdrawal;

public class WithdrawalCommandHandler(
    ITransactionsRepository transactionsRepository, IAccountService accountService, ILogger<DepositCommandHandler> logger,
    IMapper mapper, TransactionApprovalChain approvalHandler
    ) : IRequestHandler<WithdrawalCommand>
{
    public async Task Handle(WithdrawalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Beginning Withdrawal Transaction");
        bool balanceDeducted = false;
        try
        {
            var transaction = mapper.Map<Transaction>(request);
            transaction.CreatedAt = DateTime.UtcNow;
            transaction.FromAccountId = request.AccountId;
            transaction.TransactionType = EnumTransactionType.Withdrawal;
            await approvalHandler.ExecuteAsync(transaction);
            if (transaction.Status == EnumTransactionStatus.Approved)
            {
                await accountService.UpdateAccount(accountId: request.AccountId, balance: -1 * transaction.Amount);
                balanceDeducted = true;
            }
            await transactionsRepository.AddAsync(transaction);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message);
            if (balanceDeducted)
            {
                await accountService.UpdateAccount(accountId: request.AccountId, balance: request.Amount);
            }
        }
    }
}
