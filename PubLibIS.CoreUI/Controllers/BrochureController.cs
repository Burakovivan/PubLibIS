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
   // [Authorize(Roles = "admin, user")]
    [Route("api/[controller]")]
    public class BrochureController : Controller
    {
        private IBrochureService service;
        private IPublishingHouseService phService;
        private IHostingEnvironment hostingEnvironment;

        public BrochureController(IBrochureService service, IPublishingHouseService phService, IHostingEnvironment hostingEnvironment)
        {
            this.service = service;
            this.phService = phService;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Brochure
        [HttpGet]
        public IEnumerable<BrochureViewModel> Get()
        {
            return service.GetBrochureViewModelList();
        }
        [HttpGet("phlist/{id}")]
        public SelectList GetPublishingHouseSelectList(int id)
        {
            var phId = service.GetBrochureViewModel(id).PublishingHouse_Id;
            var selectList = new SelectList();
            selectList.Items = new List<SelectListItem>();
            phService.GetPublishingHouseViewModelSlimList().ToList()
                .ForEach(ph =>
                selectList.Items.Add(new SelectListItem {Value = ph.Id, Text = ph.Description, Selected = ph.Id == phId }));
            return selectList;
        }

        [HttpGet("{id}")]
        public BrochureViewModel Details(int id)
        {
            return service.GetBrochureViewModel(id);
        }

        [HttpPut]
      //  [Authorize(Roles = "admin")]
        public BrochureViewModel Edit([FromBody]BrochureViewModel Brochure)
        {
            service.UpdateBrochure(Brochure);
            return service.GetBrochureViewModel(Brochure.Id);
        }

       // [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.DeleteBrochure(id);
            return Ok(id);
        }

     
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public BrochureViewModel Create([FromBody]BrochureViewModel Brochure)
        {
            if(Brochure.ReleaseDate == DateTime.MinValue)
            {
                return null;
            }
            var id = service.CreateBrochure(Brochure);
            return service.GetBrochureViewModel(id);
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

    //[Authorize(Roles = "admin")]
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
