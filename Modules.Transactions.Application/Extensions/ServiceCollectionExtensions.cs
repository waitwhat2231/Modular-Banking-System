using Common.SharedClasses.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Transactions.Application.CommittingStrategies;
using Modules.Transactions.Application.CommittingStrategies.Factory;
using Modules.Transactions.Application.Handlers;
using Modules.Transactions.Application.Services;


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
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddTransient<TransactionApprovalChain>(provider =>
            {
                var auto = provider.GetRequiredService<AutoApprovalTransactionHandler>();
                var mgr = provider.GetRequiredService<AdministratorApprovalTransactionHandler>();
                var adm = provider.GetRequiredService<ManagerApprovalHandler>();

                auto.SetNext(mgr).SetNext(adm);

                return new TransactionApprovalChain(auto);
            });

            services.AddScoped<TransferStrategy>();
            services.AddScoped<WithdrawalStrategy>();
            services.AddScoped<DepositStrategy>();

            services.AddScoped<ITransactionStrategyFactory, TransactionStrategyFactory>();
        }
    }
}