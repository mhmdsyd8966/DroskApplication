using Microsoft.EntityFrameworkCore;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Education.System.IServices.IdentityService;
using Education.System.Core.Views;
using Education.System.Core.Dto.TeacherDto;

namespace Education.System.Services.ApplicationService
{
    public class PackageService(TheLayerContext context, ITeacherService teacherService) : IPackageService
    {
        public async Task AddPackage(PackageDto packageDto)
        {
            var package = new Package
            {
                PackageName = packageDto.Name,
                PackagePrice = packageDto.PackagePrice,
                TeacherId = packageDto.TeacherId,
                NumberOfLessons = 0,
                Students = [],
                NumberOfStudents = 0,
                PackagePhoto = packageDto.PackagePhoto
            };

            await context.Packages.AddAsync(package);
            await context.SaveChangesAsync();

            await teacherService.IncrementNumberOfPackagesBy(package.TeacherId, 1);
        }

        public async Task EditPackage(Guid id, EditPackageDto packageDto)
        {
            var package = await GetPackageById(id);

            package.PackagePrice = packageDto.PackagePrice != 0 ? packageDto.PackagePrice : package.PackagePrice;
            package.PackageName = string.IsNullOrEmpty(packageDto.Name) ? package.PackageName : packageDto.Name;
            package.PackagePhoto = string.IsNullOrEmpty(packageDto.PackagePhoto)
                ? package.PackagePhoto
                : packageDto.PackagePhoto;
            context.Packages.Update(package);
            await context.SaveChangesAsync();
        }

        public async Task RemovePackage(Guid id)
        {
            var package = await GetPackageById(id);
            context.Packages.Remove(package);
            await context.SaveChangesAsync();

            await teacherService.IncrementNumberOfPackagesBy(package.TeacherId, -1);
        }

        public async Task<List<TeacherPackage>> GetAllPackagesByTeacherId(string id)
        {
            var packages = await context.TeacherPackages.Where(x => x.TeacherId == id).ToListAsync();
            return packages;
        }

        public async Task<List<TeacherPackage>> GetRandomPackages()
        {
            return await context.TeacherPackages
                .OrderBy(x => Guid.NewGuid())
                .Take(2)
                .ToListAsync();
        }


        public async Task<List<TeacherPackageForStudentDto>> GetAllPackagesByTeacherIdForStudent(string teacherId,
            string studentId)
        {
            var studentpackages = context.StudentPackages.Where(x => x.StudentId.Equals(studentId));
            var packages = await context.TeacherPackages.Where(x => x.TeacherId == teacherId)
                .Select(x => new TeacherPackageForStudentDto()
                {
                    TeacherPackage = x,
                    Flag = studentpackages.Any(p => p.PackageId.Equals(x.PackageId))
                }).ToListAsync();
            return packages;
        }

        public async Task<List<StudentPackage>> GetAllSubscribedPackagesForStudent(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                throw new NullReferenceException("Id can't be null");
            var packages = await context.StudentPackages.Where(x => x.StudentId == studentId).ToListAsync();
            if (packages == null)
                throw new NullReferenceException("You don't have any package");
            return packages;
        }

        public async Task<Package> GetPackageById(Guid id) =>
            await context.Packages.FirstOrDefaultAsync(g => g.Id.Equals(id)) ??
            throw new Exception("Can't find package with id " + id);

        public async Task<bool> AddPackageToStudentManual(string studentId, Guid packaguId)
        {
            var student = await context.Students.Include(s => s.Packages)
                .FirstOrDefaultAsync(s => s.Id.Equals(studentId)) ?? throw new Exception("No student with this id");
            var package = await GetPackageById(packaguId);
            student.Packages.Add(package);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task IncrementNumberOfLessonsBy(Guid packageId, int num)
        {
            var package = await GetPackageById(packageId);
            if (package.NumberOfLessons + num < 0)
            {
                throw new Exception(
                    "this number is bigger than lessons in package, please check the number and try again");
            }

            package.NumberOfLessons += num;
            context.Packages.Update(package);

            await context.SaveChangesAsync();
        }


    }
}
