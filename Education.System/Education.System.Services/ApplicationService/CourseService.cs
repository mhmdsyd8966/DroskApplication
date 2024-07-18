using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService
{
    public class CourseService(TheLayerContext context) : ICourseService
    {
        public async Task<Course> AddCourse(CourseDto model)
        {
            var course = new Course
            {
                CourseName = model.Name,
                NumberOfTeachers = 0,
                CoursePhoto = model.CoursePhoto
            };
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> DeleteCourse(Guid id)
        {
            var course = await GetCourseById(id);
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return course;

        }

        public async Task AddTeacherToCourse(Guid courseId)
        {
            var course = await GetCourseById(courseId);
            course.NumberOfTeachers++;
            context.Courses.Update(course);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTeacherFromCourse(Guid courseId)
        {
            var course = await GetCourseById(courseId);
            if (course.NumberOfTeachers == 0)
            {
                throw new Exception("Course has no teachers");
            }
            course.NumberOfTeachers--;
            context.Courses.Update(course);
            await context.SaveChangesAsync();
        }

        public async Task<Course> EditCourse(Guid id, CourseDto model)
        {
            var course = await GetCourseById(id);
            course.CourseName = string.IsNullOrEmpty(model.Name) ? course.CourseName : model.Name;
            course.CoursePhoto = string.IsNullOrEmpty(model.CoursePhoto) ? course.CoursePhoto : model.CoursePhoto;
            context.Courses.Update(course);
            await context.SaveChangesAsync();
            return course;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(Guid id)
        {
            return await context.Courses.FirstOrDefaultAsync(x => x.Id == id) ??
                   throw new Exception("Can't find course with this id");
        }



    }
}
