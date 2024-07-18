using Education.System.Core.Dto.ApplicationDto;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Client.ViewModels;

public class AddExamModelView
{
    [Required]
    [MinLength(3)]
    public string Name { set; get; }

    [Required]
    [DataType(DataType.Url)]
    public string ExamLink { set; get; }

    public ExamDto ToDto(string teacherId) => new()
    {
        Name = Name,
        ExamLink = ExamLink,
        TeacherId = teacherId
    };
}