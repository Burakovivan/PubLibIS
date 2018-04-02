using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using PubLibIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using PubLibIS.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using PubLibIS.BLL.Services;

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/book")]
  public class BookController : Controller
  {
    private BookService service;
    private AuthorService authorService;
    private IHostingEnvironment hostingEnvironment;

    public BookController(BookService service, AuthorService authorService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.hostingEnvironment = hostingEnvironment;
      this.authorService = authorService;
    }

    // GET: Book
    [HttpGet]
    public IEnumerable<BookViewModel> Get()
    {
       return service.GetBookViewModelList();
    }

    [AllowAnonymous]
    [HttpGet("getcatalog")]
    public BookCatalogViewModel GetCatalog([FromQuery]int? skip, [FromQuery]int? take)
    {
      take = take ?? 0;
      if (!skip.HasValue)
      {
        return service.GetBookCatalogViewModel(0, take.Value);
      }

      return service.GetBookCatalogViewModel(skip.Value, take.Value);

    }

    [HttpGet("authorList/{bookId}")]
    public IActionResult GetAuthorSelectList(int bookId)
    {
      IEnumerable<int> bookAuthorIdList = bookId <= 0 ? new List<int>(): authorService.GetAuthorIdListByBook(bookId);
      IEnumerable<AuthorViewModel> authorList = authorService.GetAuthorViewModelList();
      var selectList = new SelectList();
      foreach (AuthorViewModel author in authorList)
      {
        selectList.Items.Add(new SelectListItem { Value = author.Id, Text = author.FullName, Selected = bookAuthorIdList.Contains(author.Id) });
      }
      return Json(selectList);
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

      int id = service.CreateBook(book);
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
      return Ok();

    }

    public class Temp
    {
      public string Json { get; set; }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("setJson")]
    public ActionResult SetJson([FromBody]Temp json)
    {
      if (json?.Json == null)
      {
        return NoContent();
      }
      service.SetJson(json.Json);
      return Ok();
    }

    private string MapLocalPath(string virtualPath)
    {
      return Path.Combine(hostingEnvironment.ContentRootPath + virtualPath);
    }
  }
}
