using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Dto.ApplicationDto;

public class EditedExamDto
{
    [AllowNull]
    [MinLength(3)]
    public string Name { set; get; }

    [AllowNull]
    [DataType(DataType.Url)]
    public string ExamLink { set; get; }

}