using PubLibIS_BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ViewModels;

namespace PubLibIS.Controllers
{
    public class BookController : Controller
    {
        private ServiceContainer service;
        public BookController()
        {
            service = ServiceContainer.GetInstance();
        }

        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.Book.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.Book.Get(id);
            ViewBag.Publications = service.PublishedBook.GetByBook(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var authors = service.Author.GetAll();
            var authorOfBooks = service.Book.GetAuthorIdsByBook(id);
            ViewBag.Authors = new List<SelectListItem>();
            foreach (var author in authors)
            {
                ViewBag.Authors.Add(new SelectListItem()
                {
                    Text = author.FullName,
                    Value = author.Id.ToString(),
                    Selected = authors.Any(a => authorOfBooks.Any(aId => a.Id == aId))
                });
            }

            var model = service.Book.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel book)
        {

            MemoryStream memstream = new MemoryStream();
            Request.InputStream.CopyTo(memstream);
            memstream.Position = 0;
            string text;
            using (StreamReader reader = new StreamReader(memstream))
            {
                text = reader.ReadToEnd();
            }
            if (!ModelState.IsValid)
            {
                var authors = service.Author.GetAll();
                if (authors.Count() > 0)
                    ViewBag.Authors = new SelectList(authors, "Id", "FullName", authors.First());

                return View(book);
            }
            service.Book.Update(book);
            return RedirectToAction("Details", new { id = book.Id });
        }

        public ActionResult Delete(int id)
        {
            service.Book.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var authors = service.Author.GetAll();

            if (authors.Count() > 0)
                ViewBag.Authors = new SelectList(authors, "Id", "FullName", authors.First());

            return View();
        }


        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                var model = service.Author.GetAll();
                if (model.Count() > 0)
                    ViewBag.Authors = new SelectList(model, "Id", "FullName", model.First());

                return View(book);
            }
            var id = service.Book.Create(book);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult CreatePublication(int id)
        {
            var book = service.Book.Get(id);
            ViewBag.Book = book;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePublication(PublishedBookViewModel pBook)
        {
            if (!ModelState.IsValid)
            {
                var book = service.Book.Get(pBook.Book.Id);
            }
            var id = service.PublishedBook.Create(pBook);
            return RedirectToAction("Details", new { id = pBook.Book.Id });
        }

        public ActionResult RemovePublication(int id)
        {
            var bookId = service.PublishedBook.Get(id).Book.Id;
            service.PublishedBook.Delete(id);
            return RedirectToAction("Details", new { id = bookId });
        }

    }
}