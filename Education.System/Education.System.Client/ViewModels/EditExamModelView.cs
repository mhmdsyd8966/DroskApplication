using Education.System.Core.Dto.ApplicationDto;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Client.ViewModels;

public class EditExamModelView
{
    public Guid Id { set; get; }
    [MinLength(3)]
    public string Name { set; get; }

    [DataType(DataType.Url)]
    public string ExamLink { set; get; }

    public EditedExamDto ToDto() => new()
    {
        Name = Name,
        ExamLink = ExamLink
    };
}