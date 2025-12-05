namespace Modules.Users.Domain.Entities.Devices
{
    public class OTP
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Consumed { get; set; }
    }
}
