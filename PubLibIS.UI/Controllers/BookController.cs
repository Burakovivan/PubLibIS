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
    public class BookController : Controller
    {
        //TODO: RESOLVE HELPER's PROBLEM
        private AuthorHelper authorHelper;
        private PublishingHouseHelper publishingHouseHelper;
        private IBookService service;
        public BookController(IBookService service, IAuthorService authorService, IPublishingHouseService publishingHouseService)
        {
            this.service = service;
            authorHelper = new AuthorHelper(authorService);
            publishingHouseHelper = new PublishingHouseHelper(publishingHouseService);
        }

        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.GetBookViewModelList();
            return View(model);
        }

        [HttpGet]
        public ActionResult BookList(int skip = 0, int take = 4)
        {
            var model = service.GetBookCatalogViewModel(skip, take);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.GetBookViewModel(id);
            model.Publications = service.GetPublishedBookViewModelListByBook(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = service.GetBookViewModel(id);
            model.AuthorsSelectList = authorHelper.GetAuthorSelectList(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Edit(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.AuthorsSelectList = authorHelper.GetAuthorSelectList(book.Id);

                return View(book);
            }
            service.UpdateBook(book);
            return RedirectToAction("Details", new { id = book.Id });
        }

        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Delete(int id)
        {
            service.DeleteBook(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create()
        {
            var model = new BookViewModel
            {
                AuthorsSelectList = authorHelper.GetAuthorSelectList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult Create(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.AuthorsSelectList = authorHelper.GetAuthorSelectList();
                return View(book);
            }
            var id = service.CreateBook(book);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult CreatePublication(int id)
        {
            var book = service.GetBookViewModel(id);
            var pBook = new PublishedBookViewModel
            {
                Book = book,
                Book_Id = book.Id,
                PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList()}
            };
            pBook.DateOfPublication = DateTime.Now;
            return PartialView(pBook);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public void CreatePublication(PublishedBookViewModel pBook)
        {
            if (!ModelState.IsValid)
            {
                var book = service.GetBookViewModel(pBook.Book.Id);
            }
            var id = service.CreatePublication(pBook);
        }

        public ActionResult PublicationList(int id)
        {
            var publications = service.GetPublishedBookViewModelListByBook(id);
            var model = new Tuple<int, IEnumerable<PublishedBookViewModel>>(id, publications);
            return PartialView("PublicationList", model);
        }

        public void RemovePublication(int id)
        {
            service.DeletePublication(id);
        }

        [HttpGet]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult EditPublication(int id)
        {
            var pBook = service.GetPublication(id);
            pBook.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
            return PartialView(pBook);
        }

        [HttpPost]
        [Authorize(Order = 1, Roles = "admin")]
        public ActionResult EditPublication(PublishedBookViewModel pBook)
        {
            if (!ModelState.IsValid)
            {
            pBook.PublishingHouseSelectList = new ViewModels.SelectList { Items = publishingHouseHelper.GetPublishingHouseSelectList().Select(el => new ViewModels.SelectListItem { Text = el.Text, Value = int.Parse(el.Value), Selected = el.Selected }).ToList() };
                return PartialView(pBook);
            }
            service.UpdatePublication(pBook);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult GetJson(IEnumerable<int> idList)
        {
            var json = service.GetJson(idList);
            if (!Directory.Exists(Server.MapPath("~/Backups/Book")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Backups/Book"));
            }
            var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
            var filePath = Server.MapPath("~/Backups/Book") + $"\\{fileName}";
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