#region

using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Adam.Core;
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
            var app = AdamContext.GetNewApp();
            Application.Add("AdamApplication", app);
            SetJobsTimer(app);
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

        protected void Application_End()
        {
            var app = Application.Get("AdamApplication") as Application;
            if (app != null)
            {
                app.LogOff();
            }
        }

        private void SetJobsTimer(Application app)
        {
            if (app != null)
            {
                var interval = int.Parse(WebConfigurationManager.AppSettings["SharedFolderJobsRetryTime"]);
                AdamSharedFolderJobManager.AddExecuteJobsTimer(app, interval);
            }
        }
    }
}