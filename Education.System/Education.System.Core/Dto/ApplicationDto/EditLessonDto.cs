using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Dto.ApplicationDto
{
    public class EditLessonDto
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public string PdfUrl { get; set; }
        public string PhotoUrl { get; set; }

        public EditLessonDto()
        {

        }

        public EditLessonDto(LessonDto model)
        {
            Name = model.Name;
            PhotoUrl = model.LessonImage;
            PdfUrl = model.PdfUrl;
            VideoUrl = model.VideoUrl;
        }
    }
}
