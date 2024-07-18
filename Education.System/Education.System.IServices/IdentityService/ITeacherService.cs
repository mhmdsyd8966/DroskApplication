using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.System.Core.Dto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace Education.System.IServices.IdentityService
{
    public interface ITeacherService
    {
        Task<PrintTeacher> SignUp(SignUpTeacherDto model);
        Task<SignInResult> SignIn(SignInDto signin);
        Task<List<TeacherReturnDto>> GetAllTeachers();
        Task<List<TeacherReturnDto>> GetAllTeacherByFilter(string? name, string? courseId);
        Task IncrementNumberOfPostsBy(string id, int number);
        Task IncrementNumberOfPackagesBy(string teacherId, int number);
        Task SignOut();
        Task<Teacher> GetTeacherById(string teacherId);
        Task<List<TeacherReturnDto>> GetRandomTeachers(int count = 5);
    }
}
