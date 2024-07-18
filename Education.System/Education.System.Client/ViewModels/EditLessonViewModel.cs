namespace Education.System.Client.ViewModels;

public class EditLessonViewModel
{
    public Guid LessonId { set; get; }
    public Guid TeacherId { set; get; }
    public string Name { get; set; }
    public string VideoUrl { get; set; }
    public string PdfUrl { get; set; }
    public Guid PackageId { get; set; }
    public string LessonImage { get; set; }
}