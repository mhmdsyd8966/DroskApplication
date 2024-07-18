using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Core.Application
{
    public class Lesson
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }

        [Url]
        public string? PdfLink { get; set; }


        public string LessonImage { get; set; }

        [Required]
        [Url]
        public string VideoLink { get; set; }

        [ForeignKey(nameof(Package))]
        public Guid PackageId { get; set; }

        public Package Package { get; set; }
    }
}