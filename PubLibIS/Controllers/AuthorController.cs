using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PubLibIS.Models.Author;
using PubLibIS_BLL.Services;
using AuthorDLL = PubLibIS_BLL.Model.Author;

namespace PubLibIS.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            AuthorService s = new AuthorService();
            var x = s.GetAllAuthors();
            var model = Mapper.Map<IEnumerable<AuthorDLL>, IEnumerable<AuthorViewModel>>(x);
            return View(model);
        }
    }
}