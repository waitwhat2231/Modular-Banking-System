using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Transactions.Domain.Repositories;
using Modules.Transactions.Infrastructure.Repositories;
using Modules.Transactions.Infrastructure.Seeders;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTransactionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //"Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"
        //  var connectionString = configuration.GetConnectionString("TemplateDb");
        //Server=db34639.public.databaseasp.net; Database=db34639; User Id=db34639; Password=3Zk@S_4o=yB8; Encrypt=True; TrustServerCertificate=True; 
        //services.AddDbContext<TransactionsDbContext>(options => options.UseSqlServer("Server=db34639.public.databaseasp.net; Database=db34639; User Id=db34639; Password=3Zk@S_4o=yB8; Encrypt=True; TrustServerCertificate=True; "));
        services.AddDbContext<TransactionsDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"));
        services.AddScoped<ITransactionRulesSeeder, TransactionRulesSeeder>();
        services.AddScoped<ITransactionRulesRepository, TransactionRulesRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();
    }
}
