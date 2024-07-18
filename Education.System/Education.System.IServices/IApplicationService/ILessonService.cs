using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Views;

namespace Education.System.IServices.IApplicationService
{
    public interface ILessonService
    {
        Task AddLesson(LessonDto lesson);
        Task EditLesson(Guid id, EditLessonDto model);
        Task DeleteLesson(Guid id);
        Task<List<PackageLesson>> GetAllLessonsByPackageId(Guid id);
        Task<PackageLesson> GetLessonById(Guid id);
    }
}
