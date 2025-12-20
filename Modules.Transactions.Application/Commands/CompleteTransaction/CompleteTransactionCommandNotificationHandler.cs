using Common.SharedClasses.Jobs;
using Common.SharedClasses.Services;
using Hangfire;
using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction;

public class CompleteTransactionCommandNotificationHandler(INotificationService notificationService,
    IUsersService usersService, IBackgroundJobClient backgroundJobClient)
    : INotificationHandler<CompleteTransactionCommand>
{
    public async Task Handle(CompleteTransactionCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(
           $"Transaction of type : {notification.Type.ToString()} of amount {notification.Amount} is completed");

        var devices = await usersService.GetUserDevices(notification.userId);
        if (devices.Any())
            backgroundJobClient.Enqueue<INotificationSendingJob>(job => job.SendNotification(devices, "Transaction completed", $"Transaction of type : {notification.Type.ToString()} of amount {notification.Amount} is completed", "TRANSACTION"));
    }
}
