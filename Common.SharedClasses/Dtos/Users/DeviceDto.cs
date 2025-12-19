namespace Common.SharedClasses.Dtos.Users;

public class DeviceDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FcmToken { get; set; } = string.Empty;
    public DateTime LastLoggedInAt { get; set; }
}
