using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class EditPost
    {
        public string? Content { get; set; }
        public string? PostImage { set; get; }
    }
}
