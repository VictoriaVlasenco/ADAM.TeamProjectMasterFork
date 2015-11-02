using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADAM.TeamProject.Startup))]
namespace ADAM.TeamProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
