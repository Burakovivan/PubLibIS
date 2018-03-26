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
   // [PublishingHouseize(Roles = "admin, user")]
    [Route("api/publishingHouse")]
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
      //  [PublishingHouseize(Roles = "admin")]
        public PublishingHouseViewModel Edit([FromBody]PublishingHouseViewModel PublishingHouse)
        {
            service.UpdatePublishingHouse(PublishingHouse);
            return service.GetPublishingHouseViewModel(PublishingHouse.Id);
        }

       // [PublishingHouseize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.DeletePublishingHouse(id);
            return Ok(id);
        }

     
        [HttpPost]
        //[PublishingHouseize(Roles = "admin")]
        public PublishingHouseViewModel Create([FromBody]PublishingHouseViewModel PublishingHouse)
        {
        
            var id = service.CreatePublishingHouse(PublishingHouse);
            return service.GetPublishingHouseViewModel(id);
        }

        //[HttpGet]
        //public ActionResult GetJson(IEnumerable<int> idList)
        //{
        //    var json = service.GetJson(idList);
        //    if (!Directory.Exists(MapLocalPath(@"/Backups/PublishingHouse")))
        //    {
        //        Directory.CreateDirectory(MapLocalPath(@"/Backups/PublishingHouse"));
        //    }
        //    var fileName = $"{DateTime.Now:dd.MM.yyyy hh-m-ss}.json";
        //    var filePath = MapLocalPath(@"/Backups/PublishingHouse") + $"\\{fileName}";
        //    System.IO.File.WriteAllText(filePath, json);
        //    var plainTextBytes = Encoding.UTF8.GetBytes(filePath);
        //    return Ok();

        //}

        //[PublishingHouseize(Roles = "admin")]
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