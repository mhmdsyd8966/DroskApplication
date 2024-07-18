using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.AdminDto
{
    public class SignUpAdminDto
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "{0} must contain a value")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
