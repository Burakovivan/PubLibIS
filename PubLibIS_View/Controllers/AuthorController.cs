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
        private ServiceContainer service;
        public AuthorController()
        {
            service = ServiceContainer.GetInstance();
        }

        // GET: Author
        public ActionResult Index()
        {
            var model = service.Author.GetAll();
            return View(model);
        }

        public ActionResult Info(int id)
        {
            var model = service.Author.Get(id);
            return View(model);
        }

        
    }
}