namespace Education.System.Core.Views;

public class StudentPackage
{
    public string StudentId { get; set; }
    public string StudentName { get; set; }
    public Guid PackageId { get; set; }
    public string PackageName { get; set; }
    public string PackagePhoto { get; set; }
    public int NumberOfLessons { set; get; }
}