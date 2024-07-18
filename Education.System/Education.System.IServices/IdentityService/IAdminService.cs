using Education.System.Core.Dto;
using Education.System.Core.Dto.AdminDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.IServices.IdentityService
{
    public interface IAdminService
    {
        public Task<SignInResult> SignIn(SignInDto signin);
        public Task<bool> SignUp(SignUpAdminDto model);
        Task SignOut();
    }
}
