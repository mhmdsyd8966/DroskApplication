using System.Security.Claims;
using Education.System.Client.ViewModels;
using Education.System.Core.Constants;
using Education.System.Core.Dto;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.TeacherDto;
using Education.System.Core.Views;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.System.Client.Controllers;

[Authorize(Roles = Roles.Teacher)]
public class TeacherController(
    IPackageService packageService,
    ITeacherService teacherService,
    IPostService postService,
    ILessonService lessonService,
    IExamServices examServices,
    IStorageService storageService) : Controller
{
    // GET
    public IActionResult TeacherHome()
    {
        EmptyReturnServiceViewData();
        var teacherDahboardDetails = new TeacherDashBoard();
        //call service for dashboard
        return View(teacherDahboardDetails);
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
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
            EmptyReturnServiceViewData();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad Input" });
            }

            //call login service
            var result = await teacherService.SignIn(model);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Can not Log in" });
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
            EmptyReturnServiceViewData();
            var posts = await postService.GetAllPost();
            return View(posts);
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(TeacherHome));
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

    public IActionResult AddPost()
    {
        EmptyReturnServiceViewData();
        var model = new AddPostModelView()
        {
            TeacherId = TeacherId
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(AddPostModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            await postService.AddPost(new PostDto()
            {
                TeacherId = model.TeacherId,
                Content = model.Content,
                PostPhoto = model.PostImage is null
                    ? string.Empty
                    : await storageService.AddPhotoToStorage(model.PostImage)
            });
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> EditPost(Guid postId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global", "Post Id must be entered, go back and try again");
            }

            var post = await postService.GetPostById(postId);
            return View(new EditPostModelView()
            {
                TeacherId = post.TeacherId,
                Id = post.Id,
                Content = post.Content,
                PostImagePath = post.PostPhoto
            });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction(nameof(PostsForTeacher), new { teacherId = TeacherId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(EditPostModelView model)
    {
        try
        {
            if (model.PostImageFile is null)
            {
                ModelState.Remove(nameof(model.PostImageFile));
            }
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            await postService.EditPost(model.Id, new EditPost()
            {
                Content = model.Content,
                PostImage = model.PostImageFile is null
                    ? model.PostImagePath
                    : await storageService.AddPhotoToStorage(model.PostImageFile)
            });
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost([FromQuery] Guid postId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            await postService.DeletePost(postId);
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public IActionResult AddPackage()
    {
        return View(new AddPackageModelView());
    }

    [HttpPost]
    public async Task<IActionResult> AddPackage(AddPackageModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            var photo = await storageService.AddPhotoToStorage(model.PackagePhoto);

            await packageService.AddPackage(new PackageDto()
            {
                TeacherId = TeacherId,
                Name = model.PackageName,
                PackagePrice = model.PackagePrice,
                PackagePhoto = photo
            });

            return Json(new { success = true });

        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> EditPackage([FromQuery] Guid packageId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global", "Package Id must be entered, go back and try again");
            }

            var package = await packageService.GetPackageById(packageId);
            if (CheckTeacherIsOwner(package.TeacherId))
            {
                //set image in return model view pls
                return View(new EditPackageModelView(package));
            }
            else
            {
                SetViewDataServiceReturnAttr("U have no premision to edit this package");
                return RedirectToAction("PackagesOfTeacher", "Home", new { teacherId = package.TeacherId });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr("U have no premision to edit this package");
            return RedirectToAction("PackagesOfTeacher", "Home",
                new { teacherId = TeacherId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditPackage(EditPackageModelView model)
    {
        try
        {
            if (model.PackagePhotoFile is null)
                ModelState.Remove(nameof(model.PackagePhotoFile));
            else
            {
                //some conditions
            }
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad input" });
            }

            if (CheckTeacherIsOwner(model.TeacherId))
            {

                await packageService.EditPackage(model.Id, new EditPackageDto()
                {
                    Name = model.PackageName,
                    PackagePrice = model.PackagePrice,
                    PackagePhoto = model.PackagePhotoFile is null
                        ? model.PackagePhotoPath
                        : await storageService.AddPhotoToStorage(model.PackagePhotoFile)
                });
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "u have no access to edit this package" });
            }
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> AddLesson(Guid packageId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global",
                    new { message = "Package Id must be entered, go back and try again" });
            }

            var package = await packageService.GetPackageById(packageId);
            if (CheckTeacherIsOwner(package.TeacherId))
            {
                var lesson = new AddLessonModelView()
                {
                    PackageId = package.Id
                };
                return View(lesson);
            }
            else
            {
                SetViewDataServiceReturnAttr("u have no premission to add to this package");
                return RedirectToAction("PackagesOfTeacher", "Home",
                    new { teacherId = TeacherId });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("PackagesOfTeacher", "Home",
                new { teacherId = TeacherId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddLesson(AddLessonModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad input" });
            }

            var package = await packageService.GetPackageById(model.PackageId);
            if (CheckTeacherIsOwner(package.TeacherId))
            {
                await lessonService.AddLesson(new LessonDto()
                {
                    Name = model.Name,
                    PackageId = model.PackageId,
                    LessonImage = await storageService.AddPhotoToStorage(model.LessonImage),
                    VideoUrl = await storageService.AddVideoToStorage(model.VideoLink),
                    PdfUrl = await storageService.AddPdfToStorage(model.PdfLink)
                });
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "u have no premission to add to this package" });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return Json(new { success = false, message = e.Message });
        }
    }

    public async Task<IActionResult> EditLesson(Guid lessonId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadInput", "Global",
                    new { message = "Lesson Id must be entered, go back and try again" });
            }

            var lesson = await lessonService.GetLessonById(lessonId);
            if (CheckTeacherIsOwner(lesson.TeacherId))
            {
                return View(new EditLessonModelView(lesson));
            }
            else
            {
                return RedirectToAction("PackagesOfTeacher", "Home",
                    new { teacherId = TeacherId });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("PackagesOfTeacher", "Home",
                new { teacherId = TeacherId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditLesson(EditLessonModelView model)
    {
        try
        {
            if (model.VideoFile is null)
                ModelState.Remove(nameof(model.VideoFile));
            else
            {
                //some conditions
            }
            if (model.LessonImageFile is null)
                ModelState.Remove(nameof(model.LessonImageFile));
            else
            {
                //some conditions
            }
            if (model.PdfFile is null)
                ModelState.Remove(nameof(model.PdfFile));
            else
            {
                //some conditions
            }
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad Input" });
            }

            var package = await packageService.GetPackageById(model.PackageId);
            if (CheckTeacherIsOwner(package.TeacherId))
            {
                await lessonService.EditLesson(model.Id, new EditLessonDto()
                {
                    Name = model.Name,
                    PhotoUrl = model.LessonImageFile is null
                        ? model.LessonImagePath
                        : await storageService.AddPhotoToStorage(model.LessonImageFile),
                    VideoUrl = model.VideoFile is null
                        ? model.VideoLink
                        : await storageService.AddVideoToStorage(model.VideoFile),
                    PdfUrl = model.PdfFile is null ? model.PdfPath : await storageService.AddPdfToStorage(model.PdfFile)
                });
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "u have no premission to edit this lesson" });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return Json(new { success = false, message = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeletePackage(Guid packageId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad Input" });
            }

            var package = await packageService.GetPackageById(packageId);
            if (CheckTeacherIsOwner(package.TeacherId))
            {
                await packageService.RemovePackage(packageId);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "u have no premission to delete this package this lesson" });
            }
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }
    [HttpPost]
    public async Task<IActionResult> DeleteLesson(Guid lessonId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Bad Input" });
            }

            var lesson = await lessonService.GetLessonById(lessonId);
            if (CheckTeacherIsOwner(lesson.TeacherId))
            {
                await lessonService.DeleteLesson(lessonId);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "u have no premission to delete this lesson" });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return Json(new { success = false, message = e.Message });
        }
    }

    public IActionResult AddExam()
    {
        return View(new AddExamModelView());
    }

    [HttpPost]
    public async Task<IActionResult> AddExam(AddExamModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                SetViewDataServiceReturnAttr("input must be valid");
                return View(model);
            }

            await examServices.AddExam(model.ToDto(TeacherId));
            return RedirectToAction("PackagesOfTeacher", new { teacherId = TeacherId });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteExam(Guid examId)
    {
        try
        {
            await examServices.DeleteExam(examId);
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    public async Task<IActionResult> EditExam(Guid examId)
    {
        try
        {
            var exam = await examServices.GetExamById(examId);
            if (!CheckTeacherIsOwner(exam.TeacherId))
            {
                return Unauthorized(new { message = "u have no access to edit this exam" });
            }
            return View(new EditExamModelView()
            {
                Name = exam.Name,
                Id = exam.Id,
                ExamLink = exam.ExamLink
            });
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return RedirectToAction("PackagesOfTeacher", new { teacherId = TeacherId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditExam(EditExamModelView model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var exam = await examServices.GetExamById(model.Id);
            if (CheckTeacherIsOwner(exam.TeacherId))
            {
                var _ = await examServices.EditExam(model.Id, model.ToDto());
                return RedirectToAction("PackagesOfTeacher", new { teacherId = TeacherId });
            }
            else
            {
                return Unauthorized(new { message = "u have no access to this edit this exam" });
            }
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr(e.Message);
            return View(model);
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
    public async Task<IActionResult> PackagesOfTeacher(string teacherId)
    {
        try
        {
            var packages = await packageService.GetAllPackagesByTeacherId(teacherId);
            return View(new TeacherPackagesModelView<TeacherPackage>()
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

    public async Task<IActionResult> Logout()
    {
        try
        {
            await teacherService.SignOut();
            return RedirectToAction("UnAuthHome", "Home");
        }
        catch (Exception e)
        {
            SetViewDataServiceReturnAttr("Can't log out");
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    private bool CheckTeacherIsOwner(string teacherId)
    {
        return teacherId == User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

    private string? TeacherId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}