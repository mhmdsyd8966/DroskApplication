using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Client.ViewModels;

public class AddCourseDtoViewModel
{
    [Display(Name = "Course Name")]
    [StringLength(maximumLength: 20, MinimumLength = 3)]
    [Required]
    public string CourseName { set; get; }
    [Display(Name = "Course Photo")]
    [AllowNull]
    public IFormFile CoursePhoto { set; get; }
}