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
  [Route("api/[controller]")]
  public class PeriodicalController : Controller
  {
    private PeriodicalService service;
    private PublishingHouseService phService;
    private IHostingEnvironment hostingEnvironment;

    public PeriodicalController(PeriodicalService service, PublishingHouseService phService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.phService = phService;
      this.hostingEnvironment = hostingEnvironment;
    }

    // GET: Periodical
    [HttpGet]
    public IEnumerable<PeriodicalViewModel> Get()
    {
      return service.GetPeriodicalViewModelList();
    }

    [AllowAnonymous]
    [HttpGet("getcatalog")]
    public PeriodicalCatalogViewModel GetCatalog([FromQuery]int? skip, [FromQuery]int? take)
    {
      take = take ?? 0;
      if (!skip.HasValue)
      {
        return service.GetPeriodicalCatalogViewModel(0, take.Value);
      }

      return service.GetPeriodicalCatalogViewModel(skip.Value, take.Value);

    }

    [HttpGet("phlist/{id}")]
    public SelectList GetPublishingHouseSelectList(int id)
    {
      int phId = id <= 0 ? 0 : service.GetPeriodicalViewModel(id).PublishingHouse_Id;
      var selectList = new SelectList();
      phService.GetPublishingHouseViewModelSlimList().ToList()
          .ForEach(ph =>
          selectList.Items.Add(new SelectListItem { Value = ph.Id, Text = ph.Description, Selected = ph.Id == phId }));
      return selectList;
    }

    [HttpGet("typelist/{id}")]
    public SelectList GetPeriodicalTypeSelectList(int id)
    {
      int typeId = id<=0? 0: service.GetPeriodicalViewModel(id).Type.Id;

      var selectList = new SelectList();

      service.GetPeriodicalTypeViewModelList().ToList()
          .ForEach(type =>
          selectList.Items.Add(new SelectListItem { Value = type.Id, Text = type.Name, Selected = type.Id == typeId }));
      return selectList;
    }

    [HttpGet("{id}")]
    public PeriodicalViewModel Details(int id)
    {
      return service.GetPeriodicalViewModel(id);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public PeriodicalViewModel Edit([FromBody]PeriodicalViewModel Periodical)
    {
      service.UpdatePeriodical(Periodical);
      return service.GetPeriodicalViewModel(Periodical.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeletePeriodical(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Create([FromBody]PeriodicalViewModel Periodical)
    {
      if (Periodical.Foundation <= new DateTime(1970, 1, 1))
      {
        return BadRequest(new { message = "Release date might be greater than passed" });
      }
      int id = service.CreatePeriodical(Periodical);
      return Json(service.GetPeriodicalViewModel(id));
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
