using Microsoft.EntityFrameworkCore;
using Modules.Accounts.Domain.Entities;

namespace Modules.Accounts.Infrastructure.Persistence;

internal class AccountsDbContext(DbContextOptions<AccountsDbContext> options) : DbContext(options)
{
    //internal DbSet<EntityType> table_name {get; set;}
    internal DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Accounts");

        //relationships between the tables
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Children)
            .WithOne()
            .HasForeignKey(a => a.ParentAccountId);


        modelBuilder.Entity<Account>()
            .HasOne(a => a.Parent)
            .WithMany()
            .HasForeignKey(a => a.ParentAccountId);
    }
}
