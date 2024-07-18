using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.IServices.IApplicationService
{
    public interface ICourseService
    {
        Task<Course> AddCourse(CourseDto model);
        Task<List<Course>> GetAllCourses();
        Task<Course> GetCourseById(Guid id);
        Task<Course> EditCourse(Guid id, CourseDto model);
        Task<Course> DeleteCourse(Guid id);
        Task AddTeacherToCourse(Guid courseId);
        Task RemoveTeacherFromCourse(Guid courseId);
    }
}
