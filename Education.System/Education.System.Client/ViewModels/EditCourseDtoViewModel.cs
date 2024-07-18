using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Client.ViewModels;

public class EditCourseDtoViewModel
{
    [Display(Name = "Course Name")]
    [StringLength(maximumLength: 20, MinimumLength = 3)]
    [AllowNull]
    public string CourseName { set; get; }
    [Display(Name = "Course Photo")]
    [AllowNull]
    public IFormFile? CoursePhoto { set; get; }
    public string? CoursePhotoPath { set; get; }
    [Display(Name = "Course Id")]
    public Guid Id { set; get; }
}