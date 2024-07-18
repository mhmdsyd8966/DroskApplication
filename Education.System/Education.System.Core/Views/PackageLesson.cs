namespace Education.System.Core.Views
{

    public class PackageLesson
    {
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public Guid LessonId { get; set; }
        public string LessonName { get; set; }
        public string PackagePhoto { get; set; }
        public string LessonPhoto { get; set; }
        public string LessonPdf { get; set; }
        public string LessonVideo { get; set; }
        public string TeacherId { set; get; }
    }
}