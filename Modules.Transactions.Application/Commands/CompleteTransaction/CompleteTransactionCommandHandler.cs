using Common.SharedClasses.Services;
using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public class CompleteTransactionCommandHandler(INotificationService notificationService, IUsersService usersService)
    : INotificationHandler<CompleteTransactionCommand>
{
    public async Task Handle(CompleteTransactionCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(
           $"Transaction with Id: {notification.TransactionId} is completed");

        var devices = await usersService.GetUserDevices(notification.userId);
        if (devices.Any())
            await notificationService.SendNotificationAsync(devices, "Transaction completed", $"Transaction with Id: {notification.TransactionId} is completed", "TRANSACTION");
    }
}
