using Education.System.Core.Identity.Base;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Core.Application
{
    public class BugReport
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Consumer consumer { get; set; }
        public string BugContent { get; set; }
        public bool Finshed { set; get; }
        public DateTime CreatedAt { set; get; }
    }
}