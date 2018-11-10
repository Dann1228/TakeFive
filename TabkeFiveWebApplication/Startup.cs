using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TabkeFiveWebApplication.Startup))]
namespace TabkeFiveWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
