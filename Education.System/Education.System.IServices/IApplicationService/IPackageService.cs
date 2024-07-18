using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Views;

namespace Education.System.IServices.IApplicationService
{
    public interface IPackageService
    {
        Task AddPackage(PackageDto packageDto);
        Task RemovePackage(Guid id);
        Task EditPackage(Guid id, EditPackageDto packageDto);
        Task<List<TeacherPackage>> GetAllPackagesByTeacherId(string id);
        Task<List<TeacherPackage>> GetRandomPackages();
        Task<List<TeacherPackageForStudentDto>> GetAllPackagesByTeacherIdForStudent(string teacherId, string studentId);
        Task<List<StudentPackage>> GetAllSubscribedPackagesForStudent(string studentId);
        Task<Package> GetPackageById(Guid id);
        Task<bool> AddPackageToStudentManual(string studentId, Guid packaguId);
        Task IncrementNumberOfLessonsBy(Guid packageId, int num);
    }
}
