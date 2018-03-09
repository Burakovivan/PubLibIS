using PubLibIS.View.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PubLibIS.View.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var model = new Tuple<string, string>(ConnectionStringResolver.CurrentConnectionString,
                ConnectionStringResolver.CurrentConnectionString == ConnectionStringResolver.DefaultConnectionString ?
                ConnectionStringResolver.TempJsonConnectionString :
                ConnectionStringResolver.DefaultConnectionString);
            return View(model);
        }

        public ActionResult SwitchConnection()
        {
            ConnectionStringResolver.SwitchConnection();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public FileResult Download(string file)
        {
            var base64EncodedBytes = Convert.FromBase64String(file);
            var filePath = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return File(System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(filePath)), "text/json");
        }
    }
}