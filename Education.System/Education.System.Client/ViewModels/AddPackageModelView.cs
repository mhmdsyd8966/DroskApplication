using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.System.Client.ViewModels;

public class AddPackageModelView
{
    [Required]
    [MaxLength(40)]
    [MinLength(3)]
    public string PackageName { get; set; }

    [Microsoft.Build.Framework.Required]
    [Range(100, double.MaxValue)]
    public double PackagePrice { get; set; }



    public IFormFile? PackagePhoto { get; set; }


}