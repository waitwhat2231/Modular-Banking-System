using Common.SharedClasses.Repositories;
using Modules.Users.Domain.Entities.Devices;

namespace Modules.Users.Domain.Repositories;

public interface IDeviceRepository : IGenericRepository<Device>
{
    Task<Device?> GetDeviceByToken(string DeviceToken, string? userId);
    public Task<List<Device>?> SearchAsync(string? deviceToken, string? userId, bool? optIn, DateTime? loggedInInAfter, DateTime? loggedInBefore);
}
