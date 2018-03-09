using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;
using PubLibIS.ViewModels;

namespace PubLibIS.View.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService service;
        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetAuthorViewModelList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetAuthorViewModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.GetAuthorViewModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel author)
        {
            service.UpdateAuthor(author);
            return RedirectToAction("Details", new { id = author.Id });
        }

        public ActionResult Delete(int id)
        {
            service.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            var id = service.CreateAuthor(author);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public ActionResult GetJson(IEnumerable<int> idList)
        {
            var json = service.GetJson(idList);
            if (!Directory.Exists(Server.MapPath("~/Backups")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups") + $"\\{fileName}";
            System.IO.File.WriteAllText(filePath, json);
            var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
            return new HttpStatusCodeResult(200);

        }
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