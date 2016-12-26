using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BarterBuddy.Presentation.Web.Startup))]
namespace BarterBuddy.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
