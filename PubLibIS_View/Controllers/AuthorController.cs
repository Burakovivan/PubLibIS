using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PubLibIS_BLL.Services;
using ViewModels.Author;

namespace PubLibIS.Controllers
{
    public class AuthorController : Controller
    {
        
        // GET: Author
        public ActionResult Index()
        {
            var s = new AuthorService();
            var model = s.GetAllAuthors();
            return View(model);
        }

        
    }
}