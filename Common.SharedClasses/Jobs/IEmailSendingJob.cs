
namespace Common.SharedClasses.Jobs
{
    public interface IEmailSendingJob
    {
        Task SendEmail(string title, string body, string userId);
    }
}
