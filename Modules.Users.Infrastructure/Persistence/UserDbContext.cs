
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Devices;

namespace Modules.Users.Infrastructure.Persistence;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options)
{
    //internal DbSet<EntityType> table_name {get; set;}
    internal DbSet<OTP> OTPs { get; set; }
    internal DbSet<Device> Devices { get; set; }
    internal DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relationships between the tables
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Users");
        modelBuilder.Entity<User>()
          .HasMany(u => u.Devices)
          .WithOne(d => d.User)
          .HasForeignKey(d => d.UserId)
          .OnDelete(DeleteBehavior.NoAction);
        // Device → Notifications
        modelBuilder.Entity<Device>()
            .HasMany(d => d.Notifications)
            .WithOne(n => n.Device)
            .HasForeignKey(n => n.DeviceId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
