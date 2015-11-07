using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SoundCloud.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            ApplicationHelper.ApplicationHelper.AdamLogOn();
        }


        //void Application_BeginRequest(object sender, EventArgs e)
        //{
        //   ApplicationHelper.ApplicationHelper.GetApplication();
        //}

      
        protected void Application_End()
        {
            ApplicationHelper.ApplicationHelper.AdamLogOff();
        }   

    }
}
