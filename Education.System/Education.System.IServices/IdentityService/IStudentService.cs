using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.System.Core.Dto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Dto.StudentDto;
using Microsoft.AspNetCore.Identity;

namespace Education.System.IServices.IdentityService
{
    public interface IStudentService
    {
        public Task<SignInResult> SignIn(SignInDto signin);
        public Task<bool> SignUp(SignUpStudentDto signUp);
        Task SignOut();
        bool StudentHasPakcage(string studentId, Guid packageId);
        Task<CheckStudentModel> GetStudentIfExist(string email);
    }
}
