using Common.SharedClasses.Dtos.Users;

namespace Common.SharedClasses.Services;

public interface INotificationService
{
    public Task SendNotificationAsync(List<DeviceDto> devices, string title, string body, string type);
}
