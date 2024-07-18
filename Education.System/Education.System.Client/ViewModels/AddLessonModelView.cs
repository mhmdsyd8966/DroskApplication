using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Client.ViewModels;

public class AddLessonModelView
{
    [Required]
    public string Name { get; set; }

    [AllowNull]
    public IFormFile PdfLink { get; set; }

    [Required]
    public IFormFile LessonImage { get; set; }

    [Required]
    public IFormFile VideoLink { get; set; }

    public Guid PackageId { get; set; }
}