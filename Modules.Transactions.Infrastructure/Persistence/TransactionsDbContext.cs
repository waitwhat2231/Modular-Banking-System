using Microsoft.EntityFrameworkCore;

namespace Template.Infrastructure.Persistence;

internal class TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : DbContext(options)
{
    //internal DbSet<EntityType> table_name {get; set;}
    internal DbSet<Modules.Transactions.Domain.Entities.Transaction> Transactions { get; set; }
    internal DbSet<Modules.Transactions.Domain.Entities.TransactionApprovalRules> TransactionRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Transactions");

        //relationships between the tables
    }
}
