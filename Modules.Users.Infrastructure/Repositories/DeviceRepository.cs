using Common.SharedClasses.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities.Devices;
using Modules.Users.Domain.Repositories;
using Modules.Users.Infrastructure.Persistence;

namespace Modules.Users.Infrastructure.Repositories;

internal class DeviceRepository(UserDbContext userDbContext) : GenericRepository<Device>(userDbContext), IDeviceRepository
{
    public async Task<List<Device>?> SearchAsync(string? deviceToken, string? userId, bool? optIn, DateTime? loggedInInAfter, DateTime? loggedInBefore)
    {
        var query = userDbContext.Devices.AsQueryable();

        if (deviceToken != null) query = query.Where(d => d.FcmToken == deviceToken);

        if (userId != null) query = query.Where(d => d.UserId == userId);

        if (optIn != null) query = query.Where(d => d.IsActive == optIn);

        if (loggedInInAfter != null) query = query.Where(d => d.LastLoggedInAt <= loggedInInAfter);

        if (loggedInBefore != null) query = query.Where(d => d.LastLoggedInAt >= loggedInBefore);

        var device = await query.ToListAsync();
        return device;
    }
    public async Task<Device?> GetDeviceByToken(string DeviceToken, string? userId)
    {
        var query = await userDbContext.Devices
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.FcmToken == DeviceToken);

        return query;
    }
}
