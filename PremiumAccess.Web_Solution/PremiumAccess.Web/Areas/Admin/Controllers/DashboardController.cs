using Microsoft.AspNetCore.Mvc;

namespace PremiumAccess.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
