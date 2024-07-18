using Microsoft.AspNetCore.Identity;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.AdminDto;
using Education.System.Core.Identity;
using Education.System.IServices.IdentityService;
using Microsoft.EntityFrameworkCore;
using Education.System.Services.Helpers;
using Education.System.Presentation.Context;
using Education.System.IServices.IApplicationService;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Dto.ResponseModel;

namespace Education.System.Services.IdentityService
{
    public class TeacherService(
        UserManager<Teacher> userManger,
        TheLayerContext context,
        ICourseService courseService,
        SignInManager<Teacher> signInManager)
        : ITeacherService
    {
        public async Task<PrintTeacher> SignUp(SignUpTeacherDto model)
        {
            if (model == null || model.TeacherImage is null || string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName))
                throw new Exception("Model can't be null");

            var email = Generator.GenerateEmail();
            while (await userManger.FindByEmailAsync(email) is not null)
            {
                email = Generator.GenerateEmail();
            }

            var newTeacher = new Teacher()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = email,
                UserName = email,
                CourseId = model.CourseId,
                Photo = model.TeacherImage
            };

            await courseService.AddTeacherToCourse(model.CourseId);
            var password = Generator.GeneratePassword();

            var response = await userManger.CreateAsync(newTeacher, password);

            if (!response.Succeeded)
                throw new Exception("Something went wrong");

            var user = await userManger.FindByEmailAsync(newTeacher.Email) ??
                       throw new Exception($"Internal Error ");

            var res2 = await userManger.AddToRoleAsync(user, Roles.Teacher);

            if (!res2.Succeeded)
                throw new Exception($"Can't add the user for role ");

            return new PrintTeacher
            {
                Email = email,
                Password = password,
            };
        }

        public async Task<SignInResult> SignIn(SignInDto signin)
        {
            if (signin == null || string.IsNullOrEmpty(signin.Email) || string.IsNullOrEmpty(signin.Password))
                throw new Exception("Model Can't Be null");
            var user = await userManger.FindByEmailAsync(signin.Email) ??
                       throw new NullReferenceException("Email or Password Wrong");

            var result = await signInManager.PasswordSignInAsync(signin.Email, signin.Password, signin.RememberMe,
                lockoutOnFailure: false);
            return result;
        }
        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<List<TeacherReturnDto>> GetAllTeachers()
        {
            var teachers = await context.Teachers.Include(x => x.Course)
                                .Select(x => new TeacherReturnDto
                                {
                                    firstName = x.FirstName,
                                    id = x.Id,
                                    lastName = x.LastName,
                                    photo = x.Photo,
                                    CourseName = x.Course.CourseName
                                }).ToListAsync();
            return teachers;
        }
        public async Task<List<TeacherReturnDto>> GetRandomTeachers(int count = 5)
        {
            var teachers = await context.Teachers.Include(x => x.Course)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(count)
                        .Select(x => new TeacherReturnDto
                        {
                            firstName = x.FirstName,
                            id = x.Id,
                            lastName = x.LastName,
                            photo = x.Photo,
                            CourseName = x.Course.CourseName
                        }).ToListAsync();
            return teachers;
        }
        public async Task<List<TeacherReturnDto>> GetAllTeacherByFilter(string? name, string? courseId)
        {
            var checkName = !string.IsNullOrEmpty(name);
            var courseIdGuid = new Guid();
            var chaeckCourse = !string.IsNullOrEmpty(courseId) && Guid.TryParse(courseId, out courseIdGuid);
            List<TeacherReturnDto> teachers = [];
            if (checkName && chaeckCourse)
            {
                teachers = await context.Teachers.Where(x =>
                        (x.FirstName.Contains(name!) || x.LastName.Contains(name!)) && x.CourseId.Equals(courseIdGuid))
                    .Include(x => x.Course).Select(x => new TeacherReturnDto
                    {
                        firstName = x.FirstName,
                        id = x.Id,
                        lastName = x.LastName,
                        photo = x.Photo,
                        CourseName = x.Course.CourseName
                    }).ToListAsync();
            }
            else if (!checkName && chaeckCourse)
            {
                teachers = await context.Teachers.Where(x => x.CourseId.Equals(courseIdGuid))
                    .Include(x => x.Course).Select(x => new TeacherReturnDto
                    {
                        firstName = x.FirstName,
                        id = x.Id,
                        lastName = x.LastName,
                        photo = x.Photo,
                        CourseName = x.Course.CourseName
                    }).ToListAsync();
            }
            else if (checkName && !chaeckCourse)
            {
                teachers = await context.Teachers.Where(x => x.FirstName.Contains(name!) || x.LastName.Contains(name!))
                    .Include(x => x.Course).Select(x => new TeacherReturnDto
                    {
                        firstName = x.FirstName,
                        id = x.Id,
                        lastName = x.LastName,
                        photo = x.Photo,
                        CourseName = x.Course.CourseName
                    }).ToListAsync();
            }
            else if (!checkName && !chaeckCourse)
            {
                teachers = await GetAllTeachers();
            }
            return teachers;
        }

        public async Task IncrementNumberOfPostsBy(string id, int number)
        {
            var teacher = await userManger.FindByIdAsync(id) ??
                          throw new Exception("Can't find teacher with id " + id);
            teacher.NumberOfPosts += number;
            await userManger.UpdateAsync(teacher);
        }
        public async Task IncrementNumberOfPackagesBy(string teacherId, int number)
        {
            var teacher = await userManger.FindByIdAsync(teacherId) ??
                          throw new Exception("Can't find teacher with id " + teacherId);
            teacher.NumberOfPackages += number;
            await userManger.UpdateAsync(teacher);
        }
        public async Task<Teacher> GetTeacherById(string teacherId) =>
            await userManger.FindByIdAsync(teacherId) ??
            throw new Exception("Can't find teacher with this id");
    }

}
