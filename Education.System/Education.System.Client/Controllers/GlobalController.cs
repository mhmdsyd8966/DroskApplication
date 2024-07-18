using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Education.System.Core.Constants;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Helpers.Enums;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Education.System.Core.Dto;

namespace Education.System.Client.Controllers;

[Authorize(Roles = $"{Roles.Admin},{Roles.Student},{Roles.Teacher}")]
public class GlobalController(
    ILessonService lessonService,
    IStudentService studentService,
    IBugReportService bugReportService) : Controller
{
    // GET
    public IActionResult BadInput(string message = "")
    {
        return View(message);
    }

    public IActionResult UserDetails()
    {
        var userDetails = new UserDetails();
        // get user details
        return View(userDetails);
    }
    public IActionResult Posts()
    {
        if (User.IsInRole(Roles.Student))
        {
            return RedirectToAction("Posts", "Student");
        }
        else if (User.IsInRole(Roles.Admin))
        {
            return RedirectToAction("Posts", "Admin");
        }
        else
        {
            return RedirectToAction("Posts", "Teacher");
        }
    }
    public IActionResult AddBugReport()
    {
        ViewBag.FormStatus = FormEditingStatus.Editing;
        EmptyReturnServiceViewData();
        return View(new BugReportDto()
        {
            UserId = UserId
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddBugReport(BugReportDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                SetViewDataFormStatusAttr(FormEditingStatus.Editing);
                SetViewDataServiceReturnAttr("Bad input");
                return View(model);
            }

            var bugReport = await bugReportService.ReportABug(model);
            if (User.IsInRole(Roles.Student) || User.IsInRole(Roles.Teacher))
                return RedirectToAction("ConfirmBugReportted", new { id = bugReport.Id });
            else
                throw new Exception("U are not student or teacher ???");

        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            SetViewDataFormStatusAttr(FormEditingStatus.Editing);
            return View(model);
        }
    }

    [Authorize(Roles = $"{Roles.Student},{Roles.Teacher}")]
    public async Task<IActionResult> ConfirmBugReportted([Required] Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UnAuthHome", "Home");
            }
            var bugReport = await bugReportService.GetBugReportById(id);
            return View(bugReport.Id);
        }
        catch (Exception e)
        {
            return RedirectToAction("UnAuthHome", "Home");
        }
    }

    public IActionResult PostsForTeacher([FromQuery] string teacherId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(BadInput), new { Message = "String can't be null" });
        }

        if (User.IsInRole(Roles.Student))
        {
            return RedirectToAction("PostsForTeacher", "Student", new { teacherId });
        }
        else if (User.IsInRole(Roles.Admin))
        {
            return RedirectToAction("PostsForTeacher", "Admin", new { teacherId });
        }
        else
        {
            return RedirectToAction("PostsForTeacher", "Teacher", new { teacherId });
        }
    }

    [Authorize(Roles = $"{Roles.Student},{Roles.Teacher}")]
    public async Task<IActionResult> LessonsOfPackage([Required] Guid packageId, string? teacherId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PackagesOfTeacher", "Home", new { teacherId });
            }

            if (User.IsInRole(Roles.Student) && CheckStudentHasPackage(packageId) ||
                User.IsInRole(Roles.Teacher) && !string.IsNullOrEmpty(teacherId) && CheckTeacherIsOwner(teacherId))
            {
                var lessons = await lessonService.GetAllLessonsByPackageId(packageId);
                ViewData["PackageId"] = packageId;
                return View(lessons);
            }

            return RedirectToAction("PackagesOfTeacher", "Home", new { teacherId });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("PackagesOfTeacher", "Home", new { teacherId });
        }
    }



    [Authorize(Roles = $"{Roles.Student},{Roles.Teacher}")]
    public async Task<IActionResult> Lesson([Required] Guid lessonId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global", "Lesson Id must be entered, go back and try again");
            }

            var lesson = await lessonService.GetLessonById(lessonId);

            if (User.IsInRole(Roles.Student) && CheckStudentHasPackage(lesson.PackageId) ||
                User.IsInRole(Roles.Teacher) && CheckTeacherIsOwner(lesson.TeacherId))
            {
                return View(lesson);
            }

            SetViewDataServiceReturnAttr("U didn't buy this package");
            return RedirectToAction("AllPackagesForStudent", "Student");
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("AllPackagesForStudent", "Student");
        }
    }


    [HttpGet]
    public IActionResult CheckTeacherIsOwnerJsonVersion(string teacherId)
    {
        return Json(new
        {
            success = User.IsInRole(Roles.Teacher) && teacherId == User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        });
    }


    [HttpGet]
    public IActionResult CheckStudentHasPackageJsonVersion(Guid packageId)
    {
        try
        {
            return Json(new
            { success = User.IsInRole(Roles.Student) && studentService.StudentHasPakcage(UserId, packageId) });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }


    private bool CheckTeacherIsOwner(string teacherId) => teacherId == User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    private bool CheckStudentHasPackage(Guid packageId) =>
        User.IsInRole(Roles.Student) && studentService.StudentHasPakcage(UserId, packageId);




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

    private string UserId => User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}