using Autofac;
using Microsoft.AspNetCore.Mvc;
using PremiumAccess.Domain.Exceptions;
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
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Content created successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating content",
                        Type = ResponseTypes.Danger
                    });
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



        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<ContentUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ContentUpdateModel model)
        {
            model.Resolve(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateContentAsync();
                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in updating content",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<ContentListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    await model.DeleteContentAsync(id); TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Content deleted successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in deleting content",
                        Type = ResponseTypes.Danger
                    });
                }
        }
            return RedirectToAction("Index");
        }


    }

    
}
