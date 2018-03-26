using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using PubLibIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using PubLibIS.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace PubLibIS.CoreUI.Controllers
{
   // [Authorize(Roles = "admin, user")]
    [Route("api/book")]
    public class BookController : Controller
    {
        private IBookService service;
        private IHostingEnvironment hostingEnvironment;

        public BookController(IBookService service, IHostingEnvironment hostingEnvironment)
        {
            this.service = service;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Book
        [HttpGet]
        public IEnumerable<BookViewModel> Get()
        {
            return service.GetBookViewModelList();
        }

        [HttpGet("{id}")]
        public BookViewModel Details(int id)
        {
            return service.GetBookViewModel(id);
        }

        [HttpPut]
      //  [Authorize(Roles = "admin")]
        public BookViewModel Edit([FromBody]BookViewModel book)
        {
            service.UpdateBook(book);
            return service.GetBookViewModel(book.Id);
        }

       // [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.DeleteBook(id);
            return Ok(id);
        }

     
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public BookViewModel Create([FromBody]BookViewModel book)
        {
            
            var id = service.CreateBook(book);
            return service.GetBookViewModel(id);
        }

        //[HttpGet]
        //public ActionResult GetJson(IEnumerable<int> idList)
        //{
        //    var json = service.GetJson(idList);
        //    if (!Directory.Exists(MapLocalPath(@"/Backups/Book")))
        //    {
        //        Directory.CreateDirectory(MapLocalPath(@"/Backups/Book"));
        //    }
        //    var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
        //    var filePath = MapLocalPath(@"/Backups/Book") + $"\\{fileName}";
        //    System.IO.File.WriteAllText(filePath, json);
        //    var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
        //    return Ok();

        //}

        //[Authorize(Roles = "admin")]
        //public ActionResult SetJson(string json)
        //{
        //    if (json != null)
        //    {
        //        service.SetJson(json);
        //    }
        //    return Redirect(Request.Headers["Referer"].ToString());
        //}

        private string MapLocalPath(string virtualPath)
        {
            return Path.Combine(hostingEnvironment.ContentRootPath + virtualPath);
        }
    }
}