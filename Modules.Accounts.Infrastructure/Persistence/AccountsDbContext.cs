using Microsoft.EntityFrameworkCore;

namespace Modules.Accounts.Infrastructure.Persistence;

internal class AccountsDbContext(DbContextOptions<AccountsDbContext> options) : DbContext(options)
{
    //internal DbSet<EntityType> table_name {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Accounts");

        //relationships between the tables
    }
}
