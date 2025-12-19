using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public class CompleteTransactionCommandHandler : INotificationHandler<CompleteTransactionCommand>
{
    public Task Handle(CompleteTransactionCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(
           $"Transaction with Id: {notification.TransactionId} is completed");

        return Task.CompletedTask;
    }
}
