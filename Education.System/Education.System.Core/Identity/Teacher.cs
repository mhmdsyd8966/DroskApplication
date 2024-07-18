using Education.System.Core.Application;
using Education.System.Core.Identity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Identity
{
    public class Teacher : Consumer
    {
        [AllowNull]
        public Course Course { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        [AllowNull]

        public List<Package> Packages { get; set; }
        [AllowNull]
        public List<Post> Posts { get; set; }
        [AllowNull]
        public List<Exam> Exams { set; get; }
        public int NumberOfPosts { get; set; }
        public int NumberOfPackages { get; set; }
    }
}