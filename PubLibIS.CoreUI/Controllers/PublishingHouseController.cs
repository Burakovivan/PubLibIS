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
  public class PublishingHouseController : Controller
  {
    private IPublishingHouseService service;
    private IHostingEnvironment hostingEnvironment;

    public PublishingHouseController(IPublishingHouseService service, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.hostingEnvironment = hostingEnvironment;
    }

    // GET: PublishingHouse
    [HttpGet]
    public IEnumerable<PublishingHouseViewModel> Get()
    {
      return service.GetPublishingHouseViewModelList();
    }

    [HttpGet("{id}")]
    public PublishingHouseViewModel Details(int id)
    {
      return service.GetPublishingHouseViewModel(id);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public PublishingHouseViewModel Edit([FromBody]PublishingHouseViewModel PublishingHouse)
    {
      service.UpdatePublishingHouse(PublishingHouse);
      return service.GetPublishingHouseViewModel(PublishingHouse.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeletePublishingHouse(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public PublishingHouseViewModel Create([FromBody]PublishingHouseViewModel PublishingHouse)
    {

      var id = service.CreatePublishingHouse(PublishingHouse);
      return service.GetPublishingHouseViewModel(id);
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
