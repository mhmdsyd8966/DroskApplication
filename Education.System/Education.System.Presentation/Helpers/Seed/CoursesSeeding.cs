using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Presentation.Helpers.Seed
{
    public static class CoursesSeeding
    {
        public static void SeedCourses(this ModelBuilder model)
        {
            //model.Entity<Course>().HasData(
            //new Course
            //{
            //    Id = Guid.NewGuid(),
            //    CourseName = "اللغة العربية"
            //},
            //new Course
            //{
            //    Id = Guid.NewGuid(),
            //    CourseName = "اللغة الانجليزية"
            //},
            //new Course
            //{
            //    Id = Guid.NewGuid(),
            //    CourseName = "رياضيات"
            //},
            //new Course
            //{
            //    Id = Guid.NewGuid(),
            //    CourseName = "فيزياء"

            //},
            //new Course
            //{
            //    Id = Guid.NewGuid(),
            //    CourseName = "كيمياء"
            //}
            //);
        }
    }
}
