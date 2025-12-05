using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities.Devices;

namespace Modules.Users.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public List<Device> Devices = [];
    }
}
