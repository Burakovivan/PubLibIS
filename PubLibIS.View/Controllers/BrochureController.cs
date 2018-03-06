using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;
using PubLibIS.View.Helpers;
using PubLibIS.ViewModels;

namespace PubLibIS.View.Controllers
{
    public class BrochureController : Controller
    {
        private IBrochureService service;
        private PublishingHouseHelper publishingHouseHelper;
        public BrochureController(IBrochureService service, IPublishingHouseService publishingHouseService)
        {
            this.service = service;
            this.publishingHouseHelper = new PublishingHouseHelper(publishingHouseService);
        }

        // GET: Brochure
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetGetBrochureViewModelList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetBrochureViewModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.GetBrochureViewModel(id);
            model.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BrochureViewModel brochure)
        {
            if (!ModelState.IsValid)
            {
                brochure.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList();
                return View(brochure);
            }
            service.UpdateBrochure(brochure);
            return RedirectToAction("Details", new { id = brochure.Id });
        }

        public ActionResult Delete(int id)
        {
            service.DeleteBrochure(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new BrochureViewModel
            {
                PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BrochureViewModel brochure)
        {
            if (!ModelState.IsValid)
            {
                brochure.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList();
                return View(brochure);
            }
            var id = service.CreateBrochure(brochure);
            return RedirectToAction("Details", new { id });
        }


    }
}