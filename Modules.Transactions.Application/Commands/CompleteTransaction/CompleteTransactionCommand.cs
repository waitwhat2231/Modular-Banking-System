using Common.SharedClasses.Enums;
using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public sealed record CompleteTransactionCommand(
    string userId,
    EnumTransactionType Type,
    int Amount
) : INotification;
