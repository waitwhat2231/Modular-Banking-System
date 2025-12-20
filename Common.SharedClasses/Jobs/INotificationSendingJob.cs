using Common.SharedClasses.Dtos.Users;

namespace Common.SharedClasses.Jobs
{
    public interface INotificationSendingJob
    {
        Task SendNotification(List<DeviceDto> devices, string title, string body, string type);
    }
}
