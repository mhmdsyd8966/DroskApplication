namespace Education.System.Core.Dto.TeacherDto
{
    public class TeacherPackagelessons
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public double PackagePrice { get; set; }

        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public byte[] LessonImage { get; set; }
        public string pdflink { get; set; }
        public string videolink { get; set; }
    }
}