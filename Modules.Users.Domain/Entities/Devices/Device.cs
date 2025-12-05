namespace Modules.Users.Domain.Entities.Devices;

public class Device
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public string FcmToken { get; set; } = string.Empty;
    public List<Notification> Notifications { get; set; } = [];
    public DateTime LastLoggedInAt { get; set; }
}
