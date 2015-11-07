using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoundCloud.WebUI.Mappers;

namespace SoundCloud.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var app = ApplicationHelper.ApplicationHelper.GetApplication();
            //var a = SoundCloud.Core.AdamRepository.GetRecords(app, "SoundCloud");
            var records = SoundCloud.Core.AdamRepository.FindRecordsSoundCloud(app);
            var modelRecords = records.Select(r => r.ToSoundModel());
            //.Select(r => r.ToSoundModel());
            return View(modelRecords);
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
    }
}