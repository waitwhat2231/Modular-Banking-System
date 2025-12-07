using Microsoft.Extensions.DependencyInjection;
using Modules.Transactions.Domain.Enums;

namespace Modules.Transactions.Application.CommittingStrategies.Factory
{
    class TransactionStrategyFactory(IServiceProvider serviceProvider) : ITransactionStrategyFactory
    {
        public ITransactionCommitStrategy ChooseTransactionStrategy(EnumTransactionType type)
        {
            return type switch
            {
                EnumTransactionType.Transfer => serviceProvider.GetRequiredService<TransferStrategy>(),
                EnumTransactionType.Withdrawal => serviceProvider.GetRequiredService<WithdrawalStrategy>(),
                EnumTransactionType.Deposit => serviceProvider.GetRequiredService<DepositStrategy>(),
                _ => throw new NotSupportedException($"No strategy for transaction type {type}")
            };
        }
    }
}
