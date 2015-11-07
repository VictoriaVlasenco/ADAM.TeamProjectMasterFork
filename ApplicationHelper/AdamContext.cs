#region

using System;
using System.Web;
using Adam.Core;

#endregion

namespace ApplicationHelper
{
    public static class AdamContext
    {
        public static void AdamLogOn()
        {
            var app = new Application();
            var status = app.LogOn("TRAINING", "Victoria_Vlasenco", "asdf100795");
            if (status != LogOnStatus.LoggedOn)
            {
                app.Dispose();
                throw new UnauthorizedAccessException();
            }
            SetApplication(app);
        }


        public static void SetApplication(Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (app.IsLoggedOn == false)
            {
                throw new ArgumentException("The provided ADAM application is not logged on.", "app");
            }
            HttpContext.Current.Session["AdamSessionId"] = app.SessionId;
            HttpContext.Current.Items["AdamApplication"] = app;
        }

        public static Application GetApplication()
        {
            var app = HttpContext.Current.Items["AdamApplication"] as Application;
            if (app != null)
            {
                return app;
            }
            var storedSessionId = HttpContext.Current.Session["AdamSessionId"];
            if (storedSessionId != null)
            {
                var sessionId = (Guid) storedSessionId;

                app = new Application();
                app.LogOn("TRAINING", sessionId);

                if (app.IsLoggedOn)
                {
                    HttpContext.Current.Items["AdamApplication"] = app;
                    return app;
                }
                app.Dispose();
            }
            return null;
        }

        public static void CleanUp()
        {
            var app = HttpContext.Current.Items["AdamApplication"] as Application;
            if (app != null)
            {
                app.Dispose();
                HttpContext.Current.Items["AdamApplication"] = null;
            }
        }

        public static void AdamLogOff()
        {
            var app = GetApplication();
            app.LogOff();
        }
    }
}