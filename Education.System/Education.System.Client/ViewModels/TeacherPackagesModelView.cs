namespace Education.System.Client.ViewModels;

public class TeacherPackagesModelView<T>
{
    public string TeacherId { set; get; }
    public List<T> Packages { set; get; }
}