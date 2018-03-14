using PubLibIS.BLL.Services;
using System.Web.Mvc;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Web;

namespace PubLibIS.View.Controllers
{
    [Authorize(Order = 2, Roles = "admin, user")]
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
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = service.GetPublishingHouseViewModel(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(PublishingHouseViewModel ph)
        {
            service.UpdatePublishingHouse(ph);
            return RedirectToAction("Details", new { id = ph.Id });
        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Delete(int id)
        {
            service.DeletePublishingHouse(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create(PublishingHouseViewModel ph)
        {
            if (!ModelState.IsValid)
            {
                return View(ph);
            }
            var id = service.CreatePublishinHouse(ph);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public ActionResult GetJson(IEnumerable<int> idList)
        {
            var json = service.GetJson(idList);
            if (!Directory.Exists(Server.MapPath("~/Backups/PublishingHouse")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups/PublishingHouse"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups/PublishingHouse") + $"\\{fileName}";
            System.IO.File.WriteAllText(filePath, json);
            var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
            return new HttpStatusCodeResult(200);

        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult SetJson(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                var reader = new StreamReader(upload.InputStream);
                string json = reader.ReadToEnd();
                service.SetJson(json);
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}