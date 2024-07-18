using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.System.Core.Views
{
    public class TeacherPackage
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public int NumberOfLessons { get; set; }
        public string TeacherPhoto { get; set; }
        public string PackagePhoto { get; set; }
        public double PackagePrice { get; set; }
        public int NumberOfStudents { get; set; }

    }
}