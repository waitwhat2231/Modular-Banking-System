
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Persistence;

internal class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options)
{
    //internal DbSet<EntityType> table_name {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Users");

        //relationships between the tables
    }
}
