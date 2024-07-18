using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Views;

namespace Education.System.Client.ViewModels;

public class HomeModelView
{
    public List<TeacherReturnDto> Teachers { set; get; }
    public List<TeacherPackage> Packages { set; get; }
}