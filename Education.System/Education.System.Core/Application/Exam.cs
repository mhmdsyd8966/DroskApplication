using Education.System.Core.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Application;

public class Exam
{
    [Key]
    public Guid Id { set; get; }
    [Required]
    [MinLength(3)]
    public string Name { set; get; }
    [Required]
    [DataType(DataType.Url)]
    public string ExamLink { set; get; }
    [AllowNull]
    public Teacher Teacher { set; get; }
    [Required]
    [ForeignKey(nameof(Teacher))]
    public string TeacherId { set; get; }
}