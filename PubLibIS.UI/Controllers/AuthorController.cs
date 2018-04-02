using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Services;
using PubLibIS.ViewModels;

namespace PubLibIS.UI.Controllers
{
    [Authorize(Order = 2, Roles = "admin, user")]
    public class AuthorController : Controller
    {
        private AuthorService service;
        public AuthorController(AuthorService service)
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
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = service.GetAuthorViewModel(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(AuthorViewModel author)
        {
            service.UpdateAuthor(author);
            return RedirectToAction("Details", new { id = author.Id });
        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Delete(int id)
        {
            service.DeleteAuthor(id);
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
        public ActionResult Create(AuthorViewModel author)
        {
            if(!ModelState.IsValid)
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
            if(!Directory.Exists(Server.MapPath("~/Backups/Author")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups/Author"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups/Author") + $"\\{fileName}";
            System.IO.File.WriteAllText(filePath, json);
            var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
            return new HttpStatusCodeResult(200);

        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult SetJson(HttpPostedFileBase upload)
        {
            if(upload == null)
            {
                return new  HttpStatusCodeResult(HttpStatusCode.NoContent);
            }

            var reader = new StreamReader(upload.InputStream);
            string json = reader.ReadToEnd();
            service.SetJson(json);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}