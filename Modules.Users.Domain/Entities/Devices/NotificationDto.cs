namespace Modules.Users.Domain.Entities.Devices;

public class NotificationDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int? DeviceId { get; set; }
    public bool Read { get; set; }
    public DateTime? CreatedAt { get; set; }
}
