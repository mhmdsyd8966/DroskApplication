using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.TeacherDto
{
    public class SignUpTeacherDto
    {
        [Required(ErrorMessage = "{0} must be entered")]
        [Display(Name = "Last Name")]
        //[RegularExpression(RegexConstants.NameRegex, ErrorMessage = "{0} must be valid")]
        [MinLength(3, ErrorMessage = "{0} must be more than {1}")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} must be entered")]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "{0} must be more than {1}")]
        //[RegularExpression(RegexConstants.NameRegex, ErrorMessage = "{0} must be valid")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} must be entered")]
        [Display(Name = "Course Id")]
        public Guid CourseId { get; set; }

        [Required]
        [Display(Name = "Teacher Image")]
        public string TeacherImage { set; get; }

    }
}
