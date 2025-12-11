namespace Modules.Accounts.Domain.JobRelatedServices
{
    public interface IInterestHandler
    {
        public Task ApplyInterestToAllAccounts();
        public Task CalculatedailyAccuredInterest();

    }
}
