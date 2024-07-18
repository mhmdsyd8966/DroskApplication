using Education.System.Core.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.System.Core.Application
{
    public class Course
    {
        [Key]

        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string CourseName { get; set; }

        public List<Teacher> Teachers { get; set; }
        public int NumberOfTeachers { get; set; }
        public string CoursePhoto { get; set; }
    }
}