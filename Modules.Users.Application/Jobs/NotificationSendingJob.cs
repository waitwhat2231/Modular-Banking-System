using Common.SharedClasses.Dtos.Users;
using Common.SharedClasses.Jobs;
using Common.SharedClasses.Services;

namespace Modules.Users.Application.Jobs
{
    public class NotificationSendingJob(INotificationService notificationService) : INotificationSendingJob
    {
        public async Task SendNotification(List<DeviceDto> devices, string title, string body, string type)
        {
            await notificationService.SendNotificationAsync(devices, title, body, type);
        }
    }
}
