using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using PubLibIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using PubLibIS.BLL.Services;

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/[controller]")]
  public class PublishedPeriodicalController : Controller
  {
    private PeriodicalService service;
    private PublishingHouseService phService;
    private IHostingEnvironment hostingEnvironment;

    public PublishedPeriodicalController(PeriodicalService service, PublishingHouseService phService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.phService = phService;
      this.hostingEnvironment = hostingEnvironment;
    }

    // GET: PeriodicalEdition
    [HttpGet("{id}")]
    public IEnumerable<PeriodicalEditionViewModel> Get(int id)
    {
      return service.GetPeriodicalEditionViewModelListByPeriodicalId(id);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public PeriodicalEditionViewModel Edit([FromBody]PeriodicalEditionViewModel periodicalEdition)
    {
      service.UpdatePeriodicalEdition(periodicalEdition);
      return service.GetPeriodicalEditionViewModel(periodicalEdition.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeletePeriodicalEdition(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public PeriodicalEditionViewModel Create([FromBody]PeriodicalEditionViewModel periodicalEdition)
    {

      int id = service.CreatePeriodicalEdition(periodicalEdition);
      return service.GetPeriodicalEditionViewModel(id);
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
