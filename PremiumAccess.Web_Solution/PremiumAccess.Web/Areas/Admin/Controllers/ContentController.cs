using Microsoft.AspNetCore.Mvc;

namespace PremiumAccess.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
