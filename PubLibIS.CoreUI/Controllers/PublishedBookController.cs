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

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/[controller]")]
  public class PublishedBookController : Controller
  {
    private IBookService service;
    private IPublishingHouseService phService;
    private IHostingEnvironment hostingEnvironment;

    public PublishedBookController(IBookService service, IPublishingHouseService phService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.phService = phService;
      this.hostingEnvironment = hostingEnvironment;
    }

    // GET: PublishedBook
    [HttpGet("{id}")]
    public IEnumerable<PublishedBookViewModel> Get(int id)
    {
      return service.GetPublishedBookViewModelListByBook(id);
    }

    [HttpGet("phlist/{id}")]
    public SelectList GetPublishingHouseSelectList(int id)
    {
      var phId = service.GetPublication(id)?.PublishingHouse_Id;
      var selectList = new SelectList();
      selectList.Items = new List<SelectListItem>();
      phService.GetPublishingHouseViewModelSlimList().ToList()
          .ForEach(ph =>
          selectList.Items.Add(new SelectListItem { Value = ph.Id, Text = ph.Description, Selected = ph.Id == phId }));
      return selectList;
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public PublishedBookViewModel Edit([FromBody]PublishedBookViewModel publishedBook)
    {
      service.UpdatePublication(publishedBook);
      return service.GetPublication(publishedBook.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeletePublication(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public PublishedBookViewModel Create([FromBody]PublishedBookViewModel publishedBook)
    {

      var id = service.CreatePublication(publishedBook);
      return service.GetPublication(id);
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
