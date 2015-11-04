#region

using System.Web.Mvc;

#endregion

namespace SoundPlayer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}