using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using PubLibIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using PubLibIS.BLL.Services;
using Microsoft.AspNet.Identity;

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize(Roles = "admin, user")]
  [Route("api/[controller]")]
  public class AuthorController : Controller
  {
    private AuthorService service;
    private BackupFileService backupFileService;
    private IHostingEnvironment hostingEnvironment;

    public AuthorController(AuthorService service, BackupFileService backupFileService, IHostingEnvironment hostingEnvironment)
    {
      this.service = service;
      this.hostingEnvironment = hostingEnvironment;
      this.backupFileService = backupFileService;
    }

    // GET: Author
    [HttpGet]
    public IEnumerable<AuthorViewModel> Get()
    {
      return service.GetAuthorViewModelList();
    }

    [HttpGet("{id}")]
    public AuthorViewModel Details(int id)
    {
      return service.GetAuthorViewModel(id);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public AuthorViewModel Edit([FromBody]AuthorViewModel author)
    {
      service.UpdateAuthor(author);
      return service.GetAuthorViewModel(author.Id);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      service.DeleteAuthor(id);
      return Ok(id);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public AuthorViewModel Create([FromBody]AuthorViewModel author)
    {
      if(author.DateOfBirth == DateTime.MinValue)
      {
        return null;
      }
      int id = service.CreateAuthor(author);
      return service.GetAuthorViewModel(id);
    }

    [HttpPost("getJson")]
    public BackupFileViewModel GetJson([FromBody]IEnumerable<int> idList)
    {
      var json = service.GetJson(idList);
      var pathToFolder = MapLocalPath($"\\Backups\\{this.GetType().Name.Replace("Controller", "")}");
      if(!Directory.Exists(pathToFolder))
      {
        Directory.CreateDirectory(pathToFolder);
      }
      var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
      var filePath = Path.Combine(pathToFolder, fileName);
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
      if(json?.Json == null)
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
