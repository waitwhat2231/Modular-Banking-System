using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Infrastructure.Persistence;

namespace Modules.Transactions.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTransactionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //  var connectionString = configuration.GetConnectionString("TemplateDb");
        services.AddDbContext<TransactionsDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"));
    }
}
