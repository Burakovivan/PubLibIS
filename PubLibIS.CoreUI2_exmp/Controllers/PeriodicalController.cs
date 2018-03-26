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
    [Route("api/Periodical")]
    public class PeriodicalController : Controller
    {
        private IPeriodicalService service;
        private IPublishingHouseService phService;
        private IHostingEnvironment hostingEnvironment;

        public PeriodicalController(IPeriodicalService service, IPublishingHouseService phService, IHostingEnvironment hostingEnvironment)
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

        [HttpGet("phlist/{id}")]
        public SelectList GetPublishingHouseSelectList(int id)
        {
            var phId = service.GetPeriodicalViewModel(id).PublishingHouse_Id;
            var selectList = new SelectList();
            selectList.Items = new List<SelectListItem>();
            phService.GetPublishingHouseViewModelSlimList().ToList()
                .ForEach(ph =>
                selectList.Items.Add(new SelectListItem {Value = ph.Id, Text = ph.Description, Selected = ph.Id == phId }));
            return selectList;
        }

        [HttpGet("typelist/{id}")]
        public SelectList GetPeriodicalTypeSelectList(int id)
        {
            var phId = service.GetPeriodicalViewModel(id).Type.Id;
            
            var selectList = new SelectList();
            selectList.Items = new List<SelectListItem>();
            service.GetPeriodicalTypeViewModelList().ToList()
                .ForEach(ph =>
                selectList.Items.Add(new SelectListItem { Value = ph.Id, Text = ph.Name, Selected = ph.Id == phId }));
            return selectList;
        }

        [HttpGet("{id}")]
        public PeriodicalViewModel Details(int id)
        {
            return service.GetPeriodicalViewModel(id);
        }

        [HttpPut]
      //  [Authorize(Roles = "admin")]
        public PeriodicalViewModel Edit([FromBody]PeriodicalViewModel Periodical)
        {
            service.UpdatePeriodical(Periodical);
            return service.GetPeriodicalViewModel(Periodical.Id);
        }

       // [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.DeletePeriodical(id);
            return Ok(id);
        }

     
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public PeriodicalViewModel Create([FromBody]PeriodicalViewModel Periodical)
        {
            
            var id = service.CreatePeriodical(Periodical);
            return service.GetPeriodicalViewModel(id);
        }

        //[HttpGet]
        //public ActionResult GetJson(IEnumerable<int> idList)
        //{
        //    var json = service.GetJson(idList);
        //    if (!Directory.Exists(MapLocalPath(@"/Backups/Periodical")))
        //    {
        //        Directory.CreateDirectory(MapLocalPath(@"/Backups/Periodical"));
        //    }
        //    var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
        //    var filePath = MapLocalPath(@"/Backups/Periodical") + $"\\{fileName}";
        //    System.IO.File.WriteAllText(filePath, json);
        //    var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
        //    return Ok();

        //}

        //[Authorize(Roles = "admin")]
        //public ActionResult SetJson(string json)
        //{
        //    if (json != null)
        //    {
        //        service.SetJson(json);
        //    }
        //    return Redirect(Request.Headers["Referer"].ToString());
        //}

        private string MapLocalPath(string virtualPath)
        {
            return Path.Combine(hostingEnvironment.ContentRootPath + virtualPath);
        }
    }
}