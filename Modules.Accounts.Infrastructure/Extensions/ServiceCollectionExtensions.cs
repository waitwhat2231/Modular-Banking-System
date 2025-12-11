using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Accounts.Domain.JobRelatedServices;
using Modules.Accounts.Domain.Repositories;
using Modules.Accounts.Infrastructure.JobRelatedServices;
using Modules.Accounts.Infrastructure.Persistence;

namespace Modules.Accounts.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAccountsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ////"Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"
        //Server=db34639.public.databaseasp.net; Database=db34639; User Id=db34639; Password=3Zk@S_4o=yB8; Encrypt=True; TrustServerCertificate=True; 
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<AccountsDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddHangfire((sp, config) =>
        {
            config.UseSqlServerStorage(connectionString);

        }
   );
        services.AddHangfireServer();

        services.AddScoped<IInterestHandler, InterestHandler>();
        services.AddSingleton<IJobScheduler, JobScheduler>();

    }
}
