namespace Modules.Users.Domain.Entities.Devices;

public class GroupedNotification
{
    public DateTime? CreatedAt { get; set; }
    public List<NotificationDto> Notifications { get; set; } = new();
}
