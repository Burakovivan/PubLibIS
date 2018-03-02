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
            service.Update(author);
            return RedirectToAction("Details", new { id = author.Id });
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
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
            var id = service.Create(author);
            return RedirectToAction("Details", new { id });
        }


    }
}