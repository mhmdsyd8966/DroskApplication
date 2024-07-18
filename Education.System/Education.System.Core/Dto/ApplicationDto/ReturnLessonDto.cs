using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class ReturnLessonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public string? PdfUrl { get; set; }

        public string LessonImage { get; set; }
        public string PackageName { get; set; }
        public Guid PackageId { get; set; }
    }
}
