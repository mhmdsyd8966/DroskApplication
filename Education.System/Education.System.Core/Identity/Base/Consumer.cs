using Education.System.Core.Application;
using Microsoft.AspNetCore.Identity;

namespace Education.System.Core.Identity.Base
{
    public class Consumer : IdentityUser
    {
        public string? Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BugReport>? BugReports { get; set; }
    }
}