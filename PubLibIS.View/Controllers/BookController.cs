using PubLibIS.View.Helpers;
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
        public ActionResult Details(int id)
        {
            var model = service.Get(id);
            model.Publications = service.GetPublishedBookViewModelListByBook(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.Get(id);

            model.AuthorsSelectList = authorHelper.GetAuthorSelectList(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel book)
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
                book.AuthorsSelectList = authorHelper.GetAuthorSelectList(book.Id);

                return View(book);
            }
            service.UpdateBook(book);
            return RedirectToAction("Details", new { id = book.Id });
        }

        public ActionResult Delete(int id)
        {
            service.DeleteBook(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new BookViewModel
            {
                AuthorsSelectList = authorHelper.GetAuthorSelectList()
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.AuthorsSelectList = authorHelper.GetAuthorSelectList();
                return View(book);
            }
            var id = service.Create(book);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult CreatePublication(int id)
        {
            var book = service.Get(id);
            var pBook = new PublishedBookViewModel
            {
                Book = book,
                PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList()
            };
            pBook.DateOfPublication = DateTime.Now;
            return PartialView(pBook);
        }

        [HttpPost]
        public void CreatePublication(PublishedBookViewModel pBook)
        {
            if (!ModelState.IsValid)
            {
                var book = service.Get(pBook.Book.Id);
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
        public ActionResult EditPublication(int id)
        {
            var pBook = service.GetPublication(id);
            pBook.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList(pBook.PublishingHouse.Id);
            return PartialView(pBook);
        }

        [HttpPost]
        public ActionResult EditPublication(PublishedBookViewModel pBook)
        {
            if (!ModelState.IsValid)
            {
                pBook.PublishingHouseSelectList = publishingHouseHelper.GetPublishingHouseSelectList(pBook.PublishingHouse.Id);
                return PartialView(pBook);
            }
            service.UpdatePublication(pBook);
            return Json(new { success = true });
        }

    }
}