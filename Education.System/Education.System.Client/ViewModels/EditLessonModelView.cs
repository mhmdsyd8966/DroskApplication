using System.Diagnostics.CodeAnalysis;
using Education.System.Core.Views;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;

namespace Education.System.Client.ViewModels;

public class EditLessonModelView
{
    public Guid Id { set; get; }
    [AllowNull]
    public string Name { get; set; }

    public IFormFile? PdfFile { get; set; }
    public string PdfPath { get; set; }

    public IFormFile? LessonImageFile { get; set; }
    public string LessonImagePath { get; set; }

    public IFormFile? VideoFile { get; set; }
    public string VideoLink { get; set; }

    public Guid PackageId { get; set; }

    public EditLessonModelView()
    {

    }

    public EditLessonModelView(PackageLesson model)
    {
        Id = model.LessonId;
        Name = model.LessonName;
        PdfPath = model.LessonPdf;
        PackageId = model.PackageId;
        VideoLink = model.LessonVideo;
        LessonImagePath = model.LessonPhoto;
    }
}