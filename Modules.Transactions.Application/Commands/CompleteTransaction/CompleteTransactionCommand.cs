using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public sealed record CompleteTransactionCommand(
    int TransactionId
) : INotification;
