using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Template.API.Extensions
{
    public class AllowAllDashboardAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
