using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.System.Core.Views
{
    public class TeacherPost
    {
        public Guid PostId { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPhoto { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Contents { get; set; }
        public string PostPhoto { get; set; }
    }
}