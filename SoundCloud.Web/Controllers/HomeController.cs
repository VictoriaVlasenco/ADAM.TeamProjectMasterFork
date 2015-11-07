#region

using System.Web.Mvc;
using ApplicationHelper;

#endregion

namespace SoundCloud.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var app = AdamContext.GetApplication();
            if (app!=null)
            {
                ViewBag.Adam = "HELLO ADAM";

            }
            return View();
        }
    }
}