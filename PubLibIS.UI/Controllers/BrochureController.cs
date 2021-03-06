﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Services;
using PubLibIS.UI.Helpers;
using PubLibIS.ViewModels;

namespace PubLibIS.UI.Controllers
{
    [Authorize(Order = 2, Roles = "admin, user")]
    public class BrochureController : Controller
    {
        private BrochureService service;
        private PublishingHouseHelper publishingHouseHelper;
        public BrochureController(BrochureService service, PublishingHouseService publishingHouseService)
        {
            this.service = service;
            this.publishingHouseHelper = new PublishingHouseHelper(publishingHouseService);
        }

        // GET: Brochure
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetBrochureViewModelList();
            return View(model);
        }
        public ActionResult BrochureList(int skip = 0, int take = 4)
        {
            var model = service.GetBrochureCatalogViewModel(skip,take);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetBrochureViewModel(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = service.GetBrochureViewModel(id);
            model.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(BrochureViewModel brochure)
        {
            if (!ModelState.IsValid)
            {
                brochure.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                return View(brochure);
            }
            service.UpdateBrochure(brochure);
            return RedirectToAction("Details", new { id = brochure.Id });
        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Delete(int id)
        {
            service.DeleteBrochure(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create()
        {
            var model = new BrochureViewModel
            {
                PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() }
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create(BrochureViewModel brochure)
        {
            if (!ModelState.IsValid)
            {
                brochure.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                return View(brochure);
            }
            var id = service.CreateBrochure(brochure);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public ActionResult GetJson(IEnumerable<int> idList)
        {
            var json = service.GetJson(idList);
            if (!Directory.Exists(Server.MapPath("~/Backups/Brochure")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups/Brochure"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups/Brochure") + $"\\{fileName}";
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