using Education.System.Core.Application;
using Education.System.Core.Dto.TeacherDto;
using Education.System.IServices.IApplicationService;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Client.ViewModels;

public class CreateTeacherModelView
{
    public List<Course> Courses { set; get; }
    [Required(ErrorMessage = "{0} must be entered")]
    [Display(Name = "Last Name")]
    //[RegularExpression(RegexConstants.NameRegex, ErrorMessage = "{0} must be valid")]
    [MinLength(3, ErrorMessage = "{0} must be more than {1}")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "{0} must be entered")]
    [Display(Name = "Last Name")]
    [MinLength(3, ErrorMessage = "{0} must be more than {1}")]
    //[RegularExpression(RegexConstants.NameRegex, ErrorMessage = "{0} must be valid")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "{0} must be entered")]
    [Display(Name = "Course Id")]
    public Guid CourseId { get; set; }

    [Required]
    [Display(Name = "Teacher Image")]
    public IFormFile TeacherImage { set; get; }

    public CreateTeacherModelView(List<Course> courses)
    {
        Courses = courses;
    }

    public CreateTeacherModelView()
    {

    }

    public async Task<SignUpTeacherDto> ToDto(IStorageService storageService)
    {
        var dto = new SignUpTeacherDto()
        {
            CourseId = CourseId,
            FirstName = FirstName,
            LastName = LastName,
            TeacherImage = await storageService.AddPhotoToStorage(TeacherImage)
        };
        return dto;
    }
}