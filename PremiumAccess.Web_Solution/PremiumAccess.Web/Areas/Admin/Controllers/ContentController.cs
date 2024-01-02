using Autofac;
using Microsoft.AspNetCore.Mvc;
using PremiumAccess.Infrastructure;
using PremiumAccess.Web.Areas.Admin.Models;


namespace PremiumAccess.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {

        private readonly ILifetimeScope _scope;
        private readonly ILogger<ContentController> _logger;

        public ContentController(ILifetimeScope scope,
       ILogger<ContentController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<ContentCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateContentAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create content");
                }
            }
            return View(model);
        }


        public async Task<JsonResult> GetContents()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<ContentListModel>();

            var data = await model.GetPagedContentsAsync(dataTablesModel);
            return Json(data);
        }


    }

    
}
