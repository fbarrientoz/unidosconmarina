using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webadmin.Startup))]
namespace webadmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
