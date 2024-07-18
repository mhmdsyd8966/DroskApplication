using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class PostDto
    {
        [Required]
        public string TeacherId { get; set; }


        [Required]
        public string Content { get; set; }
        public string PostPhoto { get; set; }
    }
}
