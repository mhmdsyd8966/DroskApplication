using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.AdminDto;
using Education.System.Core.Identity;
using Education.System.IServices.IdentityService;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Education.System.Presentation.Context;
using Education.System.Core.Dto.StudentDto;
using Education.System.Core.Dto.ResponseModel;

namespace Education.System.Services.IdentityService
{
    public class StudentService(
        UserManager<Student> userManger,
        TheLayerContext context,
        SignInManager<Student> signInManager)
        : IStudentService
    {
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

        public async Task<bool> SignUp(SignUpStudentDto signUp)
        {
            if (signUp == null)
                throw new ArgumentNullException("Model can't be null");

            var student = await userManger.FindByEmailAsync(signUp.Email);

            if (student != null)
            {
                throw new Exception("Email Exist for another account");
            }

            var newStudent = new Student
            {
                Email = signUp.Email,
                UserName = signUp.Email,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
            };

            var response = await userManger.CreateAsync(newStudent, signUp.Password);

            if (!response.Succeeded)
                throw new Exception("Something went wrong");

            var user = await userManger.FindByEmailAsync(newStudent.Email) ??
                       throw new Exception("Internal Error, please call support");

            var res2 = await userManger.AddToRoleAsync(user, Roles.Student);

            if (!res2.Succeeded)
                throw new Exception($"Can't add the user for role ");
            return true;
        }

        public async Task<CheckStudentModel> GetStudentIfExist(string email)
        {
            var student = await userManger.FindByEmailAsync(email);
            return student is null
                ? new CheckStudentModel()
                {
                    IsReal = false
                }
                : new CheckStudentModel()
                {
                    IsReal = true,
                    Id = student.Id,
                    Email = student.Email
                };
        }

        public bool StudentHasPakcage(string studentId, Guid packageId) =>
            context.StudentPackages.Any(sp => sp.StudentId.Equals(studentId) && sp.PackageId.Equals(packageId));

    }
}
