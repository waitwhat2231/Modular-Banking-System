using Common.SharedClasses.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Accounts.Application.Services;


namespace Template.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAccountsApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddAutoMapper(applicationAssembly);
        services.AddScoped<IAccountService, AccountService>();
    }
}