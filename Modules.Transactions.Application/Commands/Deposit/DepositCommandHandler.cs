using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Transactions.Application.Handlers;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Commands.Deposit
{
    public class DepositCommandHandler(
        ITransactionsRepository transactionsRepository, IAccountService accountService, ILogger<DepositCommandHandler> logger,
        IMapper mapper, TransactionApprovalChain approvalHandler) : IRequestHandler<DepositCommand, TransactionDto>
    {
        public async Task<TransactionDto> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Beginning Deposit Transaction");
            bool balanceAdded = false;
            try
            {
                var transaction = mapper.Map<Transaction>(request);
                transaction.CreatedAt = DateTime.UtcNow;
                transaction.TransactionType = EnumTransactionType.Deposit;
                transaction.ToAccountId = request.AccountId;
                await approvalHandler.ExecuteAsync(transaction);
                if (transaction.Status == EnumTransactionStatus.Approved)
                {
                    await accountService.UpdateAccount(accountId: request.AccountId, balance: transaction.Amount);
                    balanceAdded = true;
                }
                await transactionsRepository.AddAsync(transaction);
                var result = mapper.Map<TransactionDto>(transaction);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await accountService.UpdateAccount(accountId: request.AccountId, balance: -1 * request.Amount);
                balanceAdded = true;
                throw;
            }

        }
    }
}
