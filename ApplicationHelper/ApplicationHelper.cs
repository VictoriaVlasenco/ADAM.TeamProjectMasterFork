using Adam.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationHelper
{
    public static class ApplicationHelper
    {

        /// <summary>
        /// This method should be called once whe the app is started.
        /// </summary>
        public static void AdamLogOn()
        {
            Application app = new Application();
            LogOnStatus status = app.LogOn("TRAINING", "Victoria_Vlasenco", "asdf100795");

            if (status != LogOnStatus.LoggedOn)
            {
                app.Dispose();
                //Response.Redirect("~/Denied.html", true);
                throw new UnauthorizedAccessException();
            }

            ApplicationHelper.SetApplication(app);

        }


        /// <summary>
        /// Sets a logged-in application for the current user.
        /// </summary>
        /// <param name="app">The ADAM application to maintain.</param>
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

            // Store the ADAM session ID in the session of the current user.
            HttpContext.Current.Session["AdamSessionId"] = app.SessionId;

            // Make sure that the ADAM application gets available immediately for the current HTTP request.
            HttpContext.Current.Items["AdamApplication"] = app;
        }

        /// <summary>
        /// Gets the ADAM application for the current HTTP request.
        /// </summary>
        /// <returns>
        /// An logged-on instance of the <see cref="Application"/> class when the user can log-on to
        /// ADAM; or <c>null</c> when the user could not be logged on.
        /// </returns>
        public static Application GetApplication()
        {
            // Check if the application has already been resolved.
            Application app = HttpContext.Current.Items["AdamApplication"] as Application;
            if (app != null)
            {
                return app;
            }

            // Check if there is a session ID available for the current user.
            object storedSessionId = HttpContext.Current.Session["AdamSessionId"];
            if (storedSessionId != null)
            {
                Guid sessionId = (Guid)storedSessionId;

                // Create a logged on application instance.
                app = new Application();
                app.LogOn("TRAINING", sessionId);

                if (app.IsLoggedOn)
                {
                    // Cache the application for the current HTTP request.
                    HttpContext.Current.Items["AdamApplication"] = app;
                    return app;
                }

                // The application will not be used when the user has not been logged on.
                app.Dispose();
            }

            return null;
        }

        /// <summary>
        /// The clean-up method ensures that a cached application gets disposed.
        /// </summary>
        /// <remarks>
        /// This method must be called at the end of every HTTP request.
        /// </remarks>
        public static void CleanUp()
        {
            // Check if the application has already been resolved.
            Application app = HttpContext.Current.Items["AdamApplication"] as Application;
            if (app != null)
            {
                // If there is an application available, ensure it gets disposed.
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
