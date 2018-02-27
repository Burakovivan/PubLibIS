using PubLibIS_BLL.Services;
using System.Web.Mvc;
using ViewModels;

namespace PubLibIS.Controllers
{
    public class PublishingHouseController : Controller
    {
        private ServiceContainer service;
        public PublishingHouseController()
        {
            service = ServiceContainer.GetInstance();
        }
        // GET: PublishingHouse
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.PublishingHouse.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.PublishingHouse.Get(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.PublishingHouse.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PublishingHouseViewModel ph)
        {
            service.PublishingHouse.Update(ph);
            return RedirectToAction("Details", new { id = ph.Id });
        }

        public ActionResult Delete(int id)
        {
            service.PublishingHouse.Delete(id);
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
            var id = service.PublishingHouse.Create(ph);
            return RedirectToAction("Details", new { id });
        }
    }
}