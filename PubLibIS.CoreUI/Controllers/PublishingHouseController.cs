using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using PubLibIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using PubLibIS.BLL.Services;
using Microsoft.AspNet.Identity;
using System.Text;

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/[controller]")]
  public class PublishingHouseController : Controller
  {
    private PublishingHouseService service;
    private BackupFileService backupFileService;
    private IHostingEnvironment hostingEnvironment;

    public PublishingHouseController(PublishingHouseService service,BackupFileService backupFileService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.hostingEnvironment = hostingEnvironment;
      this.backupFileService = backupFileService;
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
