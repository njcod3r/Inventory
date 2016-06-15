using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_ERP.Startup))]
namespace MVC_ERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
