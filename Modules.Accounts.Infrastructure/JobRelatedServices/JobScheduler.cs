using Hangfire;
using Modules.Accounts.Domain.JobRelatedServices;

namespace Modules.Accounts.Infrastructure.JobRelatedServices;

public class JobScheduler(IRecurringJobManager jobManager) : IJobScheduler
{
    public void RegisterJobs()
    {
        jobManager.AddOrUpdate<InterestHandler>("CalculateInterest", job => job.ApplyInterestToAllAccounts(), "0 15 1 * *");

        jobManager.AddOrUpdate<InterestHandler>("ApplyInterest", job => job.CalculatedailyAccuredInterest(), "* 0 * * *");
    }
}
