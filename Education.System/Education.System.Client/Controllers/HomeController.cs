using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using Education.System.Client.ViewModels;
using Education.System.Core.Constants;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.System.Client.Controllers;

[AllowAnonymous]
public class HomeController(
    ITeacherService teacherService,
    IPackageService packageService,
    IExamServices examServices,
    IAdvertisementServices advertisementServices,
    ICourseService courseService) : Controller
{

    public async Task<IActionResult> UnAuthHome()
    {
        EmptyReturnServiceViewData();
        if (!User.Identity!.IsAuthenticated)
        {
            var model = new HomeModelView()
            {
                Teachers = await teacherService.GetAllTeachers(),
                Packages = await packageService.GetRandomPackages()
            };
            return View(model);
        }

        if (User.IsInRole(Roles.Student))
        {
            RedirectToAction("StudentHome", "Student");
        }
        if (User.IsInRole(Roles.Teacher))
        {
            RedirectToAction("TeacherHome", "Teacher");
        }
        if (User.IsInRole(Roles.Admin))
        {
            RedirectToAction("AdminHome", "Admin");
        }

        return View();
    }
    public IActionResult Index()
    {
        return RedirectToAction("UnAuthHome");
    }


    [HttpGet]
    public async Task<IActionResult> GetTeachersInJson()
    {
        try
        {
            var teachers = await teacherService.GetRandomTeachers();
            return Json(teachers);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetPackagesInJson()
    {
        try
        {
            var advs = await advertisementServices.GetAllAdvertisements();
            return Json(advs);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    // Action to get exams partial view
    public async Task<IActionResult> GetExamsPartial(string teacherId)
    {
        try
        {
            var exams = await examServices.GetExamsOfTeacher(teacherId);
            return PartialView("Components/_Exams", exams);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    public async Task<IActionResult> AllTeachers(string? name, string? courseId)
    {
        try
        {
            ViewBag.Courses = await courseService.GetAllCourses();
            EmptyReturnServiceViewData();
            var teachers = await teacherService.GetAllTeacherByFilter(name, courseId);

            return View(teachers);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View();
        }
    }


    [HttpPost]
    public async Task<IActionResult> FilterTeachers(string? teacherName, string? courseId)
    {
        try
        {
            var teachers = await teacherService.GetAllTeacherByFilter(teacherName, courseId);
            return PartialView("Components/_FilteredTeachers", teachers);
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> PackagesOfTeacher([FromQuery][Required] string teacherId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AllTeachers", "Home");
            }
            EmptyReturnServiceViewData();
            if (!User.Identity!.IsAuthenticated)
            {
                return View(await packageService.GetAllPackagesByTeacherId(teacherId));
            }
            if (User.IsInRole(Roles.Student))
            {
                return RedirectToAction("PackagesOfTeacher", "Student", new { teacherId });
            }
            else if (User.IsInRole(Roles.Teacher))
            {
                return RedirectToAction("PackagesOfTeacher", "Teacher",
                    new
                    {
                        teacherId = string.IsNullOrEmpty(teacherId) ? User.FindFirst(ClaimTypes.NameIdentifier)!.Value : teacherId
                    });
            }
            else
            {
                return RedirectToAction("PackagesOfTeacher", "Admin",
                    new
                    {
                        teacherId = string.IsNullOrEmpty(teacherId) ? User.FindFirst(ClaimTypes.NameIdentifier)!.Value : teacherId
                    });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("AllTeachers");
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void EmptyReturnServiceViewData()
    {
        ViewData["ServiceReturn"] = "";
    }

    private void SetViewDataServiceReturnAttr(object value)
    {
        ViewData["ServiceReturn"] = value;
    }
}
