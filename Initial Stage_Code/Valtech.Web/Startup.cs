using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValtechBaseLine.Web.Startup))]
namespace ValtechBaseLine.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}