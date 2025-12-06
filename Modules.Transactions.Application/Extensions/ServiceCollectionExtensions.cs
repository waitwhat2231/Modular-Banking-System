using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Transactions.Application.Handlers;


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
            services.AddTransient<AutoApprovalTransactionHandler>();
            services.AddTransient<AdministratorApprovalTransactionHandler>();
            services.AddTransient<ManagerApprovalHandler>();

            services.AddTransient<TransactionApprovalChain>(provider =>
            {
                var auto = provider.GetService<AutoApprovalTransactionHandler>();
                var mgr = provider.GetService<AdministratorApprovalTransactionHandler>();
                var adm = provider.GetService<ManagerApprovalHandler>();

                auto.SetNext(mgr).SetNext(adm);

                return new TransactionApprovalChain(auto);
            });
        }
    }
}