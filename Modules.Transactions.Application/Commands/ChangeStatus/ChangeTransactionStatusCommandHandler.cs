using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Transactions.Application.CommittingStrategies.Factory;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Commands.ChangeStatus
{
    class ChangeTransactionStatusCommandHandler(ITransactionStrategyFactory transactionStrategyFactory,
        ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<ChangeTransactionStatusCommand>
    {
        public async Task Handle(ChangeTransactionStatusCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            var transaction = await transactionsRepository.FindByIdAsync(request.TransactionId) ?? throw new NotFoundException("transaction", request.TransactionId.ToString());
            if (currentUser is null ||
    (!currentUser.isInRole(nameof(EnumRoleNames.Administrator)) && transaction.Status == EnumTransactionStatus.PendingAdmin
    ))
            {
                throw new ForbiddenException("Approving this transaction");
            }
            if (request.NewStatus == EnumTransactionStatus.Approved)
            {
                var strategy = transactionStrategyFactory.ChooseTransactionStrategy(transaction.TransactionType);
                await strategy.CommitTransactionAsync(transaction, currentUser.Id);
            }
            else
            {
                transaction.Status = request.NewStatus;
                await transactionsRepository.SaveChangesAsync();
            }
        }
    }
}
