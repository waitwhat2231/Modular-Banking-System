using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace Modules.Transactions.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransactionsApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddValidatorsFromAssembly(applicationAssembly)
                    .AddFluentValidationAutoValidation();

            services.AddAutoMapper(applicationAssembly);
        }
    }
}