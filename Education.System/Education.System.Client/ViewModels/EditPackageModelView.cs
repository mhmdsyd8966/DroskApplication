using Education.System.Core.Application;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Client.ViewModels;

public class EditPackageModelView
{
    public Guid Id { get; set; }

    [MaxLength(40)]
    [MinLength(3)]
    [AllowNull]
    public string PackageName { get; set; }

    [Range(100, double.MaxValue)]
    public double PackagePrice { get; set; }

    public string TeacherId { get; set; }

    public IFormFile? PackagePhotoFile { get; set; }

    public string? PackagePhotoPath { get; set; }

    public EditPackageModelView(Package model)
    {
        Id = model.Id;
        TeacherId = model.TeacherId;
        PackageName = model.PackageName;
        PackagePrice = model.PackagePrice;
        PackagePhotoPath = model.PackagePhoto;
    }

    public EditPackageModelView()
    {

    }
}