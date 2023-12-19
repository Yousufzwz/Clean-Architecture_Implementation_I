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

        public IActionResult Create()
        {
            return View();
        }

    }

    
}
