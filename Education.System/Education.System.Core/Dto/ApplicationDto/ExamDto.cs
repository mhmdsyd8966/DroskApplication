using Education.System.Core.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Dto.ApplicationDto;

public class ExamDto
{
    [Required]
    [MinLength(3)]
    public string Name { set; get; }

    [Required]
    [DataType(DataType.Url)]
    public string ExamLink { set; get; }

    [Required]
    public string TeacherId { set; get; }

    public Exam ToModel() => new()
    {
        Name = Name,
        ExamLink = ExamLink,
        TeacherId = TeacherId
    };
}