using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;
using Modules.Users.Infrastructure.Persistence;
using Modules.Users.Infrastructure.Repositories;
using Modules.Users.Infrastructure.Seeders;

namespace Modules.Users.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        //"Server=(localdb)\\mssqllocaldb;Database=BankingSystemDb;Trusted_Connection=True;"
        //Server=db34639.public.databaseasp.net; Database=db34639; User Id=db34639; Password=3Zk@S_4o=yB8; Encrypt=True; TrustServerCertificate=True; 
        //services.AddDbContext<UserDbContext>(options => options.UseSqlServer("Server=db34639.public.databaseasp.net; Database=db34639; User Id=db34639; Password=3Zk@S_4o=yB8; Encrypt=True; TrustServerCertificate=True; "));
        services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

        //this for identity and jwt when needed
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("TemplateTokenProvidor")
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IOTPRepository, OTPRepository>();
        services.AddScoped<IRolesSeeder, RolesSeeder>();
    }
}
