using Common.SharedClasses.Jobs;
using Common.SharedClasses.Services;

namespace Modules.Users.Application.Jobs
{
    public class EmailSendingJob(IUsersService userService) : IEmailSendingJob
    {
        public async Task SendEmail(string title, string body, string userId)
        {
            await userService.SendEmail(userId, title, body);
        }
    }
}
