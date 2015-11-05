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
        /// Gets the ADAM application for the current HTTP request.
        /// </summary>
        /// <returns>
        /// An logged-on instance of the <see cref="Application"/> class when the user can log-on to
        /// ADAM; or <c>null</c> when the user could not be logged on.
        /// </returns>
        public static Application GetApplication()
        {
            Application app = HttpContext.Current.Items["AdamApplication"] as Application;
            if (app != null)
                return app;

            app = new Application();
            app.LogOn("username", "password");
            if (app.IsLoggedOn)
            {
                HttpContext.Current.Items["AdamApplication"] = app;
                return app;
            }
            app.Dispose();
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
    }

}
