using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class PackageDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(100, double.MaxValue)]
        public double PackagePrice { get; set; }


        [Required]
        public string TeacherId { get; set; }
        public string PackagePhoto { get; set; }


    }
}
