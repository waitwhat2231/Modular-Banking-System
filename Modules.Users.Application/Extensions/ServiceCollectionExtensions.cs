using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace Modules.Users.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUsersApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddAutoMapper(applicationAssembly);
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
    }

}