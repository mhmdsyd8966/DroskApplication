using Education.System.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.TeacherDto
{
    public class ReturnTeacherDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string photo { get; set; }
        public List<Course> Courses { get; set; }
    }
}
