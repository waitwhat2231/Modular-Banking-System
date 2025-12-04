using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Accounts.Infrastructure.Persistence;

namespace Modules.Accounts.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAccountsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("TemplateDb");
        services.AddDbContext<AccountsDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"));
    }
}
