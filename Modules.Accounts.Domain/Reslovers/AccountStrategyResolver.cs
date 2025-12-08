using Common.SharedClasses.Enums;
using Microsoft.Extensions.DependencyInjection;
using Modules.Accounts.Domain.Strategies;
using Modules.Accounts.Infrastructure.Strategies;

namespace Modules.Accounts.Domain.Reslovers;

public interface IAccountStrategyResolver
{
    IAccountStrategy Resolve(AccountType type);
}

public class AccountStrategyResolver : IAccountStrategyResolver
{
    private readonly IServiceProvider _provider;

    public AccountStrategyResolver(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IAccountStrategy Resolve(AccountType type)
    {
        return type switch
        {
            AccountType.Saving => _provider.GetRequiredService<SavingAccountStrategy>(),
            AccountType.Checking => _provider.GetRequiredService<CheckingAccountStrategy>(),
            AccountType.Loan => _provider.GetRequiredService<LoanAccountStrategy>(),
            AccountType.Investment => _provider.GetRequiredService<InvestmentAccountStrategy>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}
