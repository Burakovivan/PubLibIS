﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PubLibIS_BLL.Services;
using ViewModels;

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
        [HttpGet]
        public ActionResult Index()
        {
            var model = service.Author.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = service.Author.Get(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = service.Author.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel author)
        {
            service.Author.Update(author);
            return RedirectToAction("Details", new { id = author.Id });
        }

        public ActionResult Delete(int id)
        {
            service.Author.Delete(id);
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
           var id =  service.Author.Create(author);
            return RedirectToAction("Details", new { id });
        } 


    }
}