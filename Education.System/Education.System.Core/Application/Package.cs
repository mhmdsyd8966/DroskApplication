using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Education.System.Core.Identity;

namespace Education.System.Core.Application
{
    public class Package
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string PackageName { get; set; }

        [Required]
        [Range(100, double.MaxValue)]
        public double PackagePrice { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        public string PackagePhoto { get; set; }

        public List<Student> Students { get; set; }
        public List<Lesson> Lessons { get; set; }
        public int NumberOfStudents { get; set; }
        public int NumberOfLessons { get; set; }
    }
}