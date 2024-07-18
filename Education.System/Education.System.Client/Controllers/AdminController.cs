using System.ComponentModel.DataAnnotations;
using Education.System.Client.ViewModels;
using Education.System.Core.Application;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.AdminDto;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Helpers.Enums;
using Education.System.Core.Views;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.System.Client.Controllers;

[Authorize(Roles = Roles.Admin)]
public class AdminController(
    ICourseService courseService,
    IAdminService adminService,
    ITeacherService teacherService,
    IBugReportService bugReportService,
    IPackageService packageService,
    IStudentService studentService,
    ICallSupportService callSupportService,
    IPostService postService,
    IAdvertisementServices advertisementServices,
    IStorageService storageService) : Controller
{

    [AllowAnonymous]
    public IActionResult Login()
    {
        SetViewDataFormStatusAttr(FormEditingStatus.Editing);
        EmptyReturnServiceViewData();
        var loginModel = new SignInDto();
        return View(loginModel);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(SignInDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad input" });
            }

            await adminService.SignIn(model);

            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        EmptyReturnServiceViewData();
        var loginModel = new SignUpAdminDto();
        return View(loginModel);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(SignUpAdminDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad input" });
            }

            var result = await adminService.SignUp(model);
            if (!result)
            {
                return Json(new { success = false, message = "Bad Request" });
            }

            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> Logout()
    {
        try
        {
            await adminService.SignOut();
            return RedirectToAction("UnAuthHome", "Home");
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    public IActionResult AdminHome()
    {
        var model = new AdminDashBoard();

        //service call to get

        return View(model);
    }
    public async Task<IActionResult> Posts()
    {
        try
        {
            EmptyReturnServiceViewData();
            var posts = await postService.GetAllPost();
            return View(posts);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(AdminHome));
        }
    }


    public async Task<IActionResult> PostsForTeacher(string teacherId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global",
                    new { message = "Teacher Id must be entered, go back and try again" });
            }

            var posts = await postService.GetAllPostsOfTeacher(teacherId);

            return View(posts);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View();
        }
    }

    public IActionResult AddingCourse()
    {
        var model = new AddCourseDtoViewModel();
        EmptyReturnServiceViewData();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddingCourse(AddCourseDtoViewModel model)
    {
        try
        {
            EmptyReturnServiceViewData();
            if (!ModelState.IsValid)
            {
                SetViewDataServiceReturnAttr("Bad input");
                return View(model);
            }

            await courseService.AddCourse(new CourseDto()
            { CoursePhoto = await storageService.AddPhotoToStorage(model.CoursePhoto), Name = model.CourseName });


            return RedirectToAction(nameof(AllCourses));
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> AllBugReports()
    {
        try
        {
            EmptyReturnServiceViewData();
            var bugReports = await bugReportService.GetAllBugReports();
            return View(bugReports);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(new List<ReturnBugReport>());
        }
    }

    [HttpPost]
    public async Task<IActionResult> EndBugReport(Guid bugReportId)
    {
        try
        {
            var success = await bugReportService.FinishBugReport(bugReportId);
            return Json(new { success, message = "Bug report Completed :)" });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> AllCallsSupport()
    {
        try
        {
            EmptyReturnServiceViewData();
            IEnumerable<CallSupport> callSupports = await callSupportService.GetAllCallSupport();
            return View(callSupports);

        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(new List<CallSupport>());
        }
    }

    public async Task<IActionResult> AllCallsSupportPartial(string filter)
    {
        try
        {
            EmptyReturnServiceViewData();
            IEnumerable<CallSupport> callSupports;

            if (filter == CallSupportStatus.Pending)
            {
                callSupports = await callSupportService.GetAllPendingCallSupport();
            }
            else
            {
                callSupports = await callSupportService.GetAllCallSupport();
            }
            return PartialView("Components/_CallSupportList", callSupports);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    [HttpPost]
    public async Task<IActionResult> ConfirmCallSupport(Guid bugReportId)
    {
        try
        {
            var success = await callSupportService.AcceptCallSupport(bugReportId);
            return Json(new { success, message = "Bug report Accepted :)" });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> RejectCallSupport(Guid bugReportId)
    {
        try
        {
            var success = await callSupportService.RejectCallSupport(bugReportId);
            return Json(new { success, message = "Bug report Rejected :(" });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> AllCourses()
    {
        try
        {
            EmptyReturnServiceViewData();
            var courses = await courseService.GetAllCourses();
            return View(courses);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(new List<Course>());
        }
    }

    public async Task<IActionResult> CreateTeacher()
    {
        try
        {
            EmptyReturnServiceViewData();
            var model = new CreateTeacherModelView(await courseService.GetAllCourses());
            return View(model);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(AdminHome));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacher(CreateTeacherModelView model)
    {
        try
        {
            ModelState.Remove(nameof(model.Courses));
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "The input is not valid" });
            }

            var result = await teacherService.SignUp(await model.ToDto(storageService));

            return Json(new { success = true, teacher = result });
            // return RedirectToAction("ShowTeacherCreatedMessage", "Admin",
            //     new { email = result.Email, password = result.Password });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public IActionResult ShowTeacherCreatedMessage([Required] string email, [Required] string password)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("BadInput", "Global");
        }
        EmptyReturnServiceViewData();
        return View(new PrintTeacher() { Email = email, Password = password });
    }

    public async Task<IActionResult> EditCourse(Guid id)
    {
        try
        {
            EmptyReturnServiceViewData();
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AllCourses));
            }

            var course = await courseService.GetCourseById(id);
            var model = new EditCourseDtoViewModel()
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CoursePhotoPath = course.CoursePhoto
            };

            return View(model);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(AllCourses));
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditCourse(EditCourseDtoViewModel model)
    {
        try
        {
            if (model.CoursePhoto is null)
            {
                ModelState.Remove(nameof(model.CoursePhoto));
            }
            if (!ModelState.IsValid)
            {
                SetViewDataServiceReturnAttr("Bad input");
                return View(model);
            }

            var _ = await courseService.EditCourse(model.Id,
                new CourseDto()
                {
                    CoursePhoto = model.CoursePhoto is null
                        ? model.CoursePhotoPath
                        : await storageService.AddPhotoToStorage(model.CoursePhoto),
                    Name = model.CourseName
                });

            return RedirectToAction(nameof(AllCourses));
        }
        catch (Exception e)
        {
            SetViewDataFormStatusAttr(FormEditingStatus.Editing);
            SetViewDataServiceReturnAttr(e.Message);
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        try
        {
            await courseService.DeleteCourse(courseId);
            SetViewDataServiceReturnAttr("deleted Successfully");
            return RedirectToAction(nameof(AllCourses));
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(AllCourses));
        }
    }
    public async Task<IActionResult> GetTeacherPackagesPartial(string teacherId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherId(teacherId);
            return PartialView("Components/_TeacherPackages", packages);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    public IActionResult PackagesOfTeacher(string teacherId)
    {
        try
        {
            return View(new TeacherPackagesModelView<TeacherPackage>()
            {
                TeacherId = teacherId,
            });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("AllTeachers", "Home");
        }
    }

    public IActionResult AddPackageToStudent(AddPackageToStudentModelView? model)
    {
        if (model is null)
        {
            EmptyReturnServiceViewData();
            return View(new AddPackageToStudentModelView());
        }

        return View();
    }



    public async Task<IActionResult> CheckStudentExist(string email)
    {
        try
        {
            var student = await studentService.GetStudentIfExist(email);
            return Json(new { success = true, studentId = student.Id });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> GetTeachersAsJson()
    {
        try
        {
            var teachers = await teacherService.GetAllTeacherByFilter(string.Empty, string.Empty);
            return Json(new { success = true, teachers });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> GetPackagesOfTeacherAsJson(string teacherId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherId(teacherId);
            return Json(new
            { success = true, packages = packages.Select(p => new { name = p.PackageName, id = p.PackageId }) });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> SubmitStudentPackage(string studentId, Guid packageId)
    {
        try
        {
            var success = await packageService.AddPackageToStudentManual(studentId, packageId);
            return Json(new { success });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> Advertisements()
    {
        try
        {
            var advertisements = await advertisementServices.GetAllAdvertisements();
            return View(advertisements);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("AdminHome");
        }
    }

    public async Task<IActionResult> AddAdvertisement(Guid pakcageId)
    {
        try
        {
            await advertisementServices.AddPackageToAdvertisements(pakcageId);
            return Json(new
            { success = true, advertisment = await advertisementServices.GetAdvertismentByPackageId(pakcageId) });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> RemoveAdvertisement(Guid advertisementId)
    {
        try
        {
            await advertisementServices.RemovePackageFromAdvertisements(advertisementId);
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> GetPackagesOfTeacherInJsonFormat(string teacherId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherId(teacherId);
            return Json(new { success = true, packages });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> GetAllTeachersInJsonFormat()
    {
        try
        {
            var teachers = await teacherService.GetAllTeacherByFilter(string.Empty, string.Empty);

            return Json(new { success = true, teachers });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }
    private void EmptyReturnServiceViewData()
    {
        ViewData["ServiceReturn"] = "";
    }
    private void SetViewDataServiceReturnAttr(object value)
    {
        ViewData["ServiceReturn"] = value;
    }
    private void SetViewDataFormStatusAttr(object value)
    {
        ViewData["FormStatus"] = value;
    }
}