using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class EditPackageDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(100, double.MaxValue)]
        public double PackagePrice { get; set; }
        public string PackagePhoto { get; set; }
        private string TeacherId;

        public EditPackageDto(string teacherId)
        {
            TeacherId = teacherId;
        }

        public EditPackageDto()
        {

        }

        public bool CompareTeacherIds(string teacherId) => TeacherId.Equals(teacherId);
    }
}
