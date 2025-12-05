using Common.SharedClasses.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities.Devices;
using Modules.Users.Domain.Repositories;
using Modules.Users.Infrastructure.Persistence;

namespace Modules.Users.Infrastructure.Repositories
{
    internal class OTPRepository : GenericRepository<OTP>, IOTPRepository
    {
        private readonly UserDbContext _userDbContext;
        public OTPRepository(UserDbContext db) : base(db)
        {
            _userDbContext = db;
        }
        public async Task<OTP> GetOtpFromCode(string code, string userId)
        {
            var otp = await _userDbContext.OTPs.FirstOrDefaultAsync(ot => ot.Code == code && ot.UserId == userId);
            return otp;
        }
    }
}
