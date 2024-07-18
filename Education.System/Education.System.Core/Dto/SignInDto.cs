using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto
{
    public class SignInDto
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [EmailAddress(ErrorMessage = "{0} must be valid")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "{0} must be valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*()-+=])(.{8,})$", ErrorMessage = "{0} must be valid")]
        [DefaultValue("@Aa123456789")]
        [MinLength(8, ErrorMessage = "{0} must be more then {1} chars.")]
        public string Password { get; set; }

        public bool RememberMe { set; get; }
        public SignInDto(string password, string email)
        {
            Password = password;
            Email = email;
        }

        public SignInDto()
        {

        }
    }
}
