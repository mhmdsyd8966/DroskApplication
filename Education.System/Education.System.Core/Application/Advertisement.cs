using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.System.Core.Application;

public class Advertisement
{
    [Key]
    public Guid PackageId { get; set; }

    [Required]
    [MaxLength(40)]
    [MinLength(3)]
    public string PackageName { get; set; }

    [Required]
    [Range(100, double.MaxValue)]
    public double PackagePrice { get; set; }

    [Required]
    public string TeacherId { get; set; }

    [Required]
    public string TeacherName { get; set; }

    public string PackagePhoto { get; set; }

    [Required]
    public int NumberOfStudentsWithPackage { get; set; }
    [Required]
    public int NumberOfLessonsInPackage { get; set; }
}