using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public sealed record CompleteTransactionCommand(
    string userId,
    int TransactionId
) : INotification;
