#region

using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ApplicationHelper;

#endregion

namespace SoundCloud.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            AdamContext.AdamLogOn();
        }

        protected void Application_EndRequest()
        {
            AdamContext.CleanUp();
        }

        protected void Session_End()
        {
            AdamContext.AdamLogOff();
        }
    }
}