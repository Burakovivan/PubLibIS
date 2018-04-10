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
using Microsoft.AspNet.Identity;

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/publishedbook")]
  public class PublishedBookController : Controller
  {
    private BookService service;
    private PublishingHouseService phService;
    private BackupFileService backupFileService;
    private IHostingEnvironment hostingEnvironment;

    public PublishedBookController(BookService service,PublishingHouseService phService, BackupFileService backupFileService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.phService = phService;
      this.hostingEnvironment = hostingEnvironment;
      this.backupFileService = backupFileService;
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
      int? phId = service.GetPublication(id)?.PublishingHouse_Id;
      var selectList = new SelectList();
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

      int id = service.CreatePublication(publishedBook);
      return service.GetPublication(id);
    }

    [HttpPost("getJson")]
    public BackupFileViewModel GetJson([FromBody]IEnumerable<int> idList)
    {
      var json = service.GetJson(idList);
      var pathToFolder = MapLocalPath($"\\Backups\\{this.GetType().Name.Replace("Controller", "")}");
      if (!Directory.Exists(pathToFolder))
      {
        Directory.CreateDirectory(pathToFolder);
      }
      var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
      var filePath = pathToFolder + $"\\{fileName}";
      System.IO.File.WriteAllText(filePath, json);
      BackupFileViewModel file = new BackupFileViewModel
      {
        FileNameBase64 = backupFileService.GetBase64EncodedFileName(fileName, Encoding.UTF8.CodePage),
        User_Id = User.Identity.GetUserId()
      };
      var fileId = backupFileService.CreateBackupFile(file, pathToFolder);
      return backupFileService.GetBackupFileViewModel(fileId);

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
