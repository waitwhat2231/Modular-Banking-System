using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Transactions.Application.Commands.Deposit;
using Modules.Transactions.Application.Events;
using Modules.Transactions.Application.Handlers;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Commands.Withdrawal;

public class WithdrawalCommandHandler(
    ITransactionsRepository transactionsRepository, IAccountService accountService, ILogger<DepositCommandHandler> logger,
    IMapper mapper, TransactionApprovalChain approvalHandler,
    IDomainEventDispatcher domainEventDispatcher) : IRequestHandler<WithdrawalCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(WithdrawalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Beginning Withdrawal Transaction");
        bool balanceDeducted = false;
        try
        {
            var account = await accountService.GetAccountFromId(request.AccountId, false);
            var transaction = mapper.Map<Transaction>(request);
            transaction.CreatedAt = DateTime.UtcNow;
            transaction.Status = EnumTransactionStatus.Rejected;
            transaction.FromAccountId = request.AccountId;
            transaction.TransactionType = EnumTransactionType.Withdrawal;
            await approvalHandler.ExecuteAsync(transaction);
            if (transaction.Status == EnumTransactionStatus.Approved)
            {
                if (transaction.Amount > account.Balance)
                {
                    throw new NoBalanceException(request.AccountId);
                }
                await accountService.UpdateAccount(accountId: request.AccountId, balance: -1 * transaction.Amount);
                balanceDeducted = true;

                transaction.Complete(account.UserId);
            }
            await transactionsRepository.AddAsync(transaction);

            await domainEventDispatcher.DispatchAsync(transaction.DomainEvents);
            transaction.ClearDomainEvents();

            var result = mapper.Map<TransactionDto>(transaction);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message);
            if (balanceDeducted)
            {
                await accountService.UpdateAccount(accountId: request.AccountId, balance: request.Amount);
            }
            throw;
        }
    }
}
