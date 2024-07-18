using Education.System.Core.Application;
using Education.System.Core.Identity;

namespace Education.System.Core.Helpers.Functions;

public static class ConvertingModels
{
    public static Advertisement FromPackageToSelectedPackage(this Package model, Teacher teacher) => new()
    {
        PackageName = model.PackageName,
        PackagePhoto = model.PackagePhoto,
        PackagePrice = model.PackagePrice,
        TeacherId = model.TeacherId,
        TeacherName = teacher.FirstName + " " + teacher.LastName,
        PackageId = model.Id,
        NumberOfLessonsInPackage = model.NumberOfLessons,
        NumberOfStudentsWithPackage = model.NumberOfStudents
    };
}