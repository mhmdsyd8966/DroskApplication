using System.Security.Claims;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.AdminDto;
using Education.System.Core.Identity;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Education.System.Services.IdentityService
{
    public class AdminService(
        UserManager<Admin> userManger,
        SignInManager<Admin> signInManager)
        : IAdminService
    {

        public async Task<bool> SignUp(SignUpAdminDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Password))
                throw new Exception("Model can't be null");

            var admin = await userManger.FindByEmailAsync(model.Email);

            if (admin != null)
            {
                throw new Exception("Email Exist for another account");
            }

            var newAdmin = new Admin
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var response = await userManger.CreateAsync(newAdmin, model.Password);

            if (!response.Succeeded)
                throw new Exception("Something went wrong");


            var user = await userManger.FindByEmailAsync(newAdmin.Email) ?? throw new Exception("something went wrong");

            var res2 = await userManger.AddToRoleAsync(user, Roles.Admin);

            if (!res2.Succeeded)
                throw new Exception($"Can't add the user for role ");
            return true;
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

    }
}
