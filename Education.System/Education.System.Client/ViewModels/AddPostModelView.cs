using Microsoft.Build.Framework;

namespace Education.System.Client.ViewModels;

public class AddPostModelView
{
    [Required]
    public string Content { get; set; }
    public IFormFile? PostImage { set; get; }
    public string TeacherId { set; get; }
}