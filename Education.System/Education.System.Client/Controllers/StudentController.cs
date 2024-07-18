using System.Security.Claims;
using Education.System.Client.ViewModels;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.StudentDto;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Helpers.Enums;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.System.Client.Controllers;

[Authorize(Roles = Roles.Student)]
public class StudentController(
    IDiscountService discountService,
    IStudentService studentService,
    IPostService postService,
    IPackageService packageService,
    IStorageService storageService,
    ICallSupportService callSupportService) : Controller
{
    public IActionResult StudentHome()
    {
        return View();
    }

    public async Task<IActionResult> AllPackagesForStudent()
    {
        try
        {
            EmptyReturnServiceViewData();
            var packages = await packageService.GetAllSubscribedPackagesForStudent(StudentId);
            return View(packages);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(StudentHome));
        }
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        var loginModel = new SignInDto();
        SetViewDataFormStatusAttr(FormEditingStatus.Editing);
        EmptyReturnServiceViewData();
        return View(loginModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(SignInDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "bad input" });
            }

            //call login service
            var result = await studentService.SignIn(model);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "can't log in" });
            }
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        var loginModel = new SignUpStudentDto();
        EmptyReturnServiceViewData();
        SetViewDataFormStatusAttr(FormEditingStatus.Editing);
        return View(loginModel);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(SignUpStudentDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "bad input" });
            }

            //call sign up service
            var result = await studentService.SignUp(model);
            if (result)
            {

                return Json(new { success = true });
            }
            else
            {
                SetViewDataServiceReturnAttr("Can't register this account");
                return Json(new { success = false, message = "can't register this account" });
            }
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> Posts()
    {
        try
        {
            var posts = await postService.GetAllPost();
            return View(posts);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(StudentHome));
        }
    }

    public async Task<IActionResult> PostsForTeacher(string teacherId)
    {
        try
        {
            var posts = await postService.GetAllPostsOfTeacher(teacherId);
            return View(posts);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(StudentHome));
        }
    }

    public async Task<IActionResult> GetAllCallSupportsForStudnet()
    {
        try
        {
            EmptyReturnServiceViewData();
            var callSupports = await callSupportService.GetAllStudentCallSupports(StudentId);
            return View(callSupports);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("StudentHome");
        }
    }

    public async Task<IActionResult> GetTeacherPackagesPartial(string teacherId, string studentId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherIdForStudent(teacherId, studentId);
            return PartialView("Components/_TeacherPackages", packages);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    public async Task<IActionResult> PackagesOfTeacher([FromQuery] string teacherId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherIdForStudent(teacherId, StudentId);
            return View(new TeacherPackagesModelView<TeacherPackageForStudentDto>()
            {
                TeacherId = teacherId,
                Packages = packages
            });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("AllTeachers", "Home");
        }
    }

    public async Task<IActionResult> MakeCallSupport(Guid packageId)
    {
        try
        {
            var package = await packageService.GetPackageById(packageId);
            return View(new AddCallSupportModelView(package.PackagePrice, packageId, StudentId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> MakeCallSupport(AddCallSupportModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                SetViewDataServiceReturnAttr("Bad Input");
                return View(model);
            }

            model.StudentId = StudentId;
            DiscountDto discount = new();
            if (!string.IsNullOrEmpty(model.DiscountCode))
            {
                discount = await discountService.GetDiscountIfAvailable(model.DiscountCode);
            }
            var package = await packageService.GetPackageById(model.PackageId);
            var price = discount.GetFinalPrice(package);
            var callSupportDto = await model.ToCallSupportDto(storageService, price);

            var callSupport = await callSupportService.AddCallSupport(callSupportDto);

            return RedirectToAction(nameof(CallSupportConfirmation), new { id = callSupport.Id });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> CallSupportConfirmation(Guid id)
    {
        try
        {
            var callSupport = await callSupportService.GetCallSupportById(id);
            return View(callSupport);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CheckDiscount(string discountCode)
    {
        try
        {
            var result = await discountService.GetDiscountIfAvailable(discountCode);
            if (!result.IsAvailable)
            {
                return Json(new { success = false, message = "this isn't real discount code" });
            }

            return Json(new { success = true, discountPercentege = result.DiscountPercentege });
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
            await studentService.SignOut();
            return RedirectToAction("UnAuthHome", "Home");
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("StudentHome");
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

    private string StudentId =>
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}