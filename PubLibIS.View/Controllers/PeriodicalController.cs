﻿using PubLibIS.View.Helpers;
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

namespace PubLibIS.View.Controllers
{
    public class PeriodicalController : Controller
    {
        private IPeriodicalService service;
        private PublishingHouseHelper publishingHouseHelper;
        private PeriodicalHelper periodicalHelper;
        public PeriodicalController(IPeriodicalService service, IPublishingHouseService publishingHouseService)
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
        public ActionResult Details(int id)
        {
            var model = service.GetPeriodicalViewModel(id);
            model.PeriodicalEditions = service.GetPeriodicalEditionViewModelByPeriodicalId(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.GetPeriodicalViewModel(id);

            model.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PeriodicalViewModel periodical)
        {

            //MemoryStream memstream = new MemoryStream();
            //Request.InputStream.CopyTo(memstream);
            //memstream.Position = 0;
            //string text;
            //using (StreamReader reader = new StreamReader(memstream))
            //{
            //    text = reader.ReadToEnd();
            //}
            if (!ModelState.IsValid)
            {
                periodical.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList(periodical.PublishingHouse?.Id);
                return View(periodical);
            }
            service.UpdatePeriodical(periodical);
            return RedirectToAction("Details", new { id = periodical.Id });
        }

        public ActionResult Delete(int id)
        {
            service.DeletePeriodical(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PeriodicalViewModel
            {
                PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(PeriodicalViewModel periodical)
        {
            if (!ModelState.IsValid)
            {
                periodical.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList(periodical.PublishingHouse?.Id);

                return View(periodical);
            }
            var id = service.CreatePeriodical(periodical);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult CreateEdition(int id)
        {
            var edition = new PeriodicalEditionViewModel
            {
                PeriodicalId = id,
                ReleaseNumber = periodicalHelper.GetNextPeriodicalEditionNumber(id),
                ReleaseDate = DateTime.Now
            };
            return PartialView(edition);
        }

        [HttpPost]
        public ActionResult CreateEdition(PeriodicalEditionViewModel edition)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotModified);
            }
            service.CreatePeriodicalEdition(edition);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult PeriodicalEditionList(int periodicalId)
        {
            var editions = service.GetPeriodicalEditionViewModelByPeriodicalId(periodicalId);
            var model = new Tuple<int, IEnumerable<PeriodicalEditionViewModel>>(periodicalId, editions);
            return PartialView( model);
        }

        public void RemovePeriodicalEdition(int id)
        {
            service.DeletePeriodicalEdition(id);
        }

        [HttpGet]
        public ActionResult EditPeriodicalEdition(int editionId)
        {
            var periodical = service.GetPeriodicalViewModel(editionId);
            return PartialView(periodical);
        }

        [HttpPost]
        public ActionResult EditPeriodicalEdition(PeriodicalEditionViewModel edition)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(edition);
            }
            service.UpdatePeriodicalEdition(edition);
            return Json(new { success = true });
        }

    }
}