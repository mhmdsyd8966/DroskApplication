using Microsoft.Build.Framework;

namespace Education.System.Client.ViewModels;

public class EditPostModelView
{
    public Guid Id { set; get; }
    [Required]
    public string Content { get; set; }
    public IFormFile? PostImageFile { set; get; }
    public string? PostImagePath { set; get; }
    public string TeacherId { set; get; }
}