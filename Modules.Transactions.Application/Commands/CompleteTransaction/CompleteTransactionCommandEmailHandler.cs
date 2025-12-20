using Common.SharedClasses.Jobs;
using Common.SharedClasses.Services;
using Hangfire;
using MediatR;

namespace Modules.Transactions.Application.Commands.CompleteTransaction
{
    public class CompleteTransactionCommandEmailHandler(IUsersService usersService, IBackgroundJobClient backgroundJobClient) : INotificationHandler<CompleteTransactionCommand>
    {
        public Task Handle(CompleteTransactionCommand notification, CancellationToken cancellationToken)
        {
            string email = $"Transaction With Id {notification.Type.ToString()}  of amount  {notification.Amount} has been completed for you";
            backgroundJobClient.Enqueue<IEmailSendingJob>(job => job.SendEmail("Transaction Complete", email, notification.userId));
            return Task.CompletedTask;
        }
    }
}
