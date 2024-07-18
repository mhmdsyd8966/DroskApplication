using Microsoft.EntityFrameworkCore;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Education.System.Core.Views;

namespace Education.System.Services.ApplicationService
{
    public class LessonService(TheLayerContext context, IPackageService packageService, IStorageService storageService) : ILessonService
    {
        public async Task AddLesson(LessonDto lesson)
        {
            var addLesson = new Lesson
            {
                Name = lesson.Name,
                PackageId = lesson.PackageId,
                PdfLink = lesson.PdfUrl,
                VideoLink = lesson.VideoUrl,
                LessonImage = lesson.LessonImage,

            };
            await context.Lessons.AddAsync(addLesson);
            await context.SaveChangesAsync();

            await packageService.IncrementNumberOfLessonsBy(lesson.PackageId, 1);
        }

        public async Task DeleteLesson(Guid id)
        {
            var lesson = await GetLesson(id);
            await packageService.IncrementNumberOfLessonsBy(lesson.PackageId, -1);
            context.Lessons.Remove(lesson);

            await storageService.DeleteFile(lesson.LessonImage);
            await storageService.DeleteFile(lesson.PdfLink);
            await storageService.DeleteFile(lesson.VideoLink);

            await context.SaveChangesAsync();
        }

        public async Task EditLesson(Guid id, EditLessonDto model)
        {
            var lesson = await GetLesson(id);
            lesson.PdfLink = string.IsNullOrEmpty(model.PdfUrl) ? lesson.PdfLink : model.PdfUrl;
            lesson.VideoLink = string.IsNullOrEmpty(model.VideoUrl) ? lesson.VideoLink : model.VideoUrl;
            lesson.Name = string.IsNullOrEmpty(model.Name) ? lesson.Name : model.Name;
            lesson.LessonImage = string.IsNullOrEmpty(model.PhotoUrl) ? lesson.LessonImage : model.PhotoUrl;
            context.Lessons.Update(lesson);
            await context.SaveChangesAsync();
        }
        public async Task<List<PackageLesson>> GetAllLessonsByPackageId(Guid id)
        {
            var lessons = await context.PackageLessons.Where(x => x.PackageId == id)
                        .ToListAsync();
            return lessons;
        }
        public async Task<PackageLesson> GetLessonById(Guid id)
        {
            var lesson = await context.PackageLessons.FirstOrDefaultAsync(y => y.LessonId == id) ??
                         throw new Exception("Can't find lesson with id " + id);
            return lesson;
        }

        private async Task<Lesson> GetLesson(Guid id) =>
            await context.Lessons.FirstOrDefaultAsync(x => x.Id == id) ??
            throw new Exception("Can't find lesson with id " + id);
    }
}
