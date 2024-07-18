using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Education.System.Core.Identity;

namespace Education.System.Core.Application
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public string PostPhoto { get; set; }

        [Required]
        public string Content { get; set; }
    }
}