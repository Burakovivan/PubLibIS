using PubLibIS.UI.Helpers;
using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.UI.Controllers
{
    [Authorize(Order = 2, Roles = "admin, user")]
    public class PeriodicalController : Controller
    {
        private PeriodicalService service;
        private PublishingHouseHelper publishingHouseHelper;
        private PeriodicalHelper periodicalHelper;
        public PeriodicalController(PeriodicalService service, PublishingHouseService publishingHouseService)
        {
            this.service = service;
            periodicalHelper = new PeriodicalHelper(service);
            publishingHouseHelper = new PublishingHouseHelper(publishingHouseService);
        }

        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetPeriodicalViewModelList();
            return View(model);
        }

        [HttpGet]
        public ActionResult PeriodicalList(int skip = 0, int take = 4)
        {
            var model = service.GetPeriodicalCatalogViewModel(skip, take);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetPeriodicalViewModel(id);
            model.PeriodicalEditions = service.GetPeriodicalEditionViewModelListByPeriodicalId(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = service.GetPeriodicalViewModel(id);
            model.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
            model.PeriodicalTypeSelectList = new ViewModels.SelectList { Items = periodicalHelper.GetPeriodicalTypeViewModelSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(PeriodicalViewModel periodical)
        {
            if (!ModelState.IsValid)
            {
                periodical.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                periodical.PeriodicalTypeSelectList = new ViewModels.SelectList { Items = periodicalHelper.GetPeriodicalTypeViewModelSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                return View(periodical);
            }
            
            service.UpdatePeriodical(periodical);
            return RedirectToAction("Details", new { id = periodical.Id });
        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Delete(int id)
        {
            service.DeletePeriodical(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create()
        {
            var model = new PeriodicalViewModel
            {
                PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() },
                PeriodicalTypeSelectList = new ViewModels.SelectList { Items = periodicalHelper.GetPeriodicalTypeViewModelSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() }
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create(PeriodicalViewModel periodical)
        {
            if (!ModelState.IsValid)
            {
                periodical.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                periodical.PeriodicalTypeSelectList = new ViewModels.SelectList { Items = periodicalHelper.GetPeriodicalTypeViewModelSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };

                return View(periodical);
            }
            var id = service.CreatePeriodical(periodical);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult CreatePeriodicalEdition(int id)
        {
            var edition = new PeriodicalEditionViewModel
            {
                Periodical_Id = id,
                ReleaseNumber = periodicalHelper.GetNextPeriodicalEditionNumber(id),
                ReleaseDate = DateTime.Now
            };
            return PartialView(edition);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult CreatePeriodicalEdition(PeriodicalEditionViewModel edition)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotModified);
            }
            service.CreatePeriodicalEdition(edition);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult PeriodicalEditionList(int id)
        {
            var editions = service.GetPeriodicalEditionViewModelListByPeriodicalId(id);
            var model = new Tuple<int, IEnumerable<PeriodicalEditionViewModel>>(id, editions);
            return PartialView(model);
        }

        public void RemovePeriodicalEdition(int id)
        {
            service.DeletePeriodicalEdition(id);
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult EditPeriodicalEdition(int id)
        {
            var periodical = service.GetPeriodicalEditionViewModel(id);
            return PartialView(periodical);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult EditPeriodicalEdition(PeriodicalEditionViewModel edition)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(edition);
            }
            service.UpdatePeriodicalEdition(edition);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult GetJson(IEnumerable<int> idList)
        {
            var json = service.GetJson(idList);
            if (!Directory.Exists(Server.MapPath("~/Backups/Periodical")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups/Periodical"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups/Periodical") + $"\\{fileName}";
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