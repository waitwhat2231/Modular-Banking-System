using Microsoft.AspNetCore.Identity;
using Modules.Users.Infrastructure.Persistence;

namespace Modules.Users.Infrastructure.Seeders;

public class RolesSeeder(UserDbContext dbContext) : IRolesSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    public IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new ()
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new ()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new ()
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                ];
        return roles;
    }
}
