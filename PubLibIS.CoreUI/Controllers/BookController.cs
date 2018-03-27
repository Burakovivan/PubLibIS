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
  [Authorize(Roles = "admin, user")]
  [Route("api/[controller]")]
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
    [Authorize(Roles = "admin")]
    public BookViewModel Edit([FromBody]BookViewModel book)
    {
      service.UpdateBook(book);
      return service.GetBookViewModel(book.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeleteBook(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public BookViewModel Create([FromBody]BookViewModel book)
    {

      var id = service.CreateBook(book);
      return service.GetBookViewModel(id);
    }

    [HttpPost("getJson")]
    public ActionResult GetJson([FromBody]IEnumerable<int> idList)
    {
      var json = service.GetJson(idList);
      var path = MapLocalPath($"\\Backups\\{this.GetType().Name.Replace("Controller", "")}");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
      var filePath = path + $"\\{fileName}";
      System.IO.File.WriteAllText(filePath, json);
      var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
      return Ok();

    }

    public class Temp
    {
      public string json { get; set; }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("setJson")]
    public ActionResult SetJson([FromBody]Temp json)
    {
      if (json == null || json.json == null)
      {
        return NoContent();
      }
      service.SetJson(json.json);
      return Ok();
    }

    private string MapLocalPath(string virtualPath)
    {
      return Path.Combine(hostingEnvironment.ContentRootPath + virtualPath);
    }
  }
}
