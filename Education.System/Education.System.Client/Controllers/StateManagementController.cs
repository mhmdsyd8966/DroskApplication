using Education.System.Core.Helpers.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Education.System.Client.Controllers;

public class StateManagementController : Controller
{
    [HttpPost]
    public IActionResult SetFormStatueToSubmitting()
    {
        ViewData["FormStatus"] = FormEditingStatus.Submitting;
        return Json(new { Message = "form submitted" });
    }
}