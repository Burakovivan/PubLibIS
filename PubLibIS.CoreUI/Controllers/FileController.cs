using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using PubLibIS.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using PubLibIS.BLL.Services;
using System.IO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PubLibIS.CoreUI.Controllers
{
  [Route("api/[controller]")]
  public class FileController : Controller
  {
    private BackupFileService backupFileService;
    private IConfiguration configuration;
    public FileController(BackupFileService backupFileService, IConfiguration configuration)
    {
      this.configuration = configuration;
      this.backupFileService = backupFileService;
    }

    [HttpGet("{file}")]
    public IActionResult GetFile(string file/*base64 string*/)
    {
      var filePath =  backupFileService.GetFilePath(file);
      var content = System.IO.File.ReadAllBytes(filePath);
      var contentType = "text/plain";
      var fileName = Path.GetFileName(filePath);

      return File(content, contentType, fileName);
    }
   
  }
}
