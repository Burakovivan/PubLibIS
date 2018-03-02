using PubLibIS.BLL.Services;
using System.Web.Mvc;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.View.Controllers
{
    public class PublishingHouseController : Controller
    {
        private IPublishingHouseService service;
        public PublishingHouseController(IPublishingHouseService service)
        {
            this.service = service;
        }
        // GET: PublishingHouse
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetPublishingHouseViewModelList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetPublishingHouseViewModel(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.GetPublishingHouseViewModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PublishingHouseViewModel ph)
        {
            service.UpdatePublishingHouse(ph);
            return RedirectToAction("Details", new { id = ph.Id });
        }

        public ActionResult Delete(int id)
        {
            service.PublishingHouse(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PublishingHouseViewModel ph)
        {
            if (!ModelState.IsValid)
            {
                return View(ph);
            }
            var id = service.CreatePublishinHouse(ph);
            return RedirectToAction("Details", new { id });
        }
    }
}