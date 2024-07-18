using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Dto.TeacherDto;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Client.ViewModels;

public class AddPackageToStudentModelView
{
    [EmailAddress]
    [Display(Name = "Student Email")]
    [Required]
    public string? Email { set; get; }
    public CheckStudentModel? ReturnedModel { set; get; }
    public List<TeacherPackageForStudentDto>? Packages { set; get; }
    [Required]
    public Guid PackageId { set; get; }

}