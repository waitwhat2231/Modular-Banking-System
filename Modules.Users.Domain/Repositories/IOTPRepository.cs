using Common.SharedClasses.Repositories;
using Modules.Users.Domain.Entities.Devices;

namespace Modules.Users.Domain.Repositories
{
    public interface IOTPRepository : IGenericRepository<OTP>
    {
        public Task<OTP> GetOtpFromCode(string code, string userId);
    }
}
