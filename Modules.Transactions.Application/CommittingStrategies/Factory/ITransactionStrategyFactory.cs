using Common.SharedClasses.Enums;

namespace Modules.Transactions.Application.CommittingStrategies.Factory
{
    public interface ITransactionStrategyFactory
    {
        public ITransactionCommitStrategy ChooseTransactionStrategy(EnumTransactionType type);
    }
}
