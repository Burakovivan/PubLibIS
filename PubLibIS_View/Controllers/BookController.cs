using PubLibIS.Helpers;
using PubLibIS_BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            model.Publications = service.PublishedBook.GetByBook(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.Book.Get(id);

            model.AuthorsSelectList = BookHelper.GetAuthorsSelectList(id);

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
                    book.AuthorsSelectList = BookHelper.GetAuthorsSelectList(book.Id);

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
            var model = new BookViewModel
            {
                AuthorsSelectList = BookHelper.GetAuthorsSelectList()
            };

            return View(model);
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
            var pBook = new PublishedBookViewModel
            {
                Book = book,
                PublishingHouseSelectList = BookHelper.GetPublishingHouseSelectList()
            };
            return PartialView(pBook);
        }

        [HttpPost]
        public ActionResult CreatePublication(PublishedBookViewModel pBook)
        {
            string documentContents;
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            if (!ModelState.IsValid)
            {
                var book = service.Book.Get(pBook.Book.Id);
            }
            var id = service.PublishedBook.Create(pBook);
            return PublicationList(pBook.Book.Id);
        }

        public ActionResult PublicationList(int id)
        {
            var publications = service.PublishedBook.GetByBook(id);
            var model = new Tuple<int, IEnumerable<PublishedBookViewModel>>(id, publications);
            return PartialView("PublicationList",model);
        }

        public ActionResult RemovePublication(int id)
        {
            var bookId = service.PublishedBook.Get(id).Book.Id;
            service.PublishedBook.Delete(id);
            return RedirectToAction("Details", new { id = bookId });
        }

    }
}