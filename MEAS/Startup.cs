using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MEAS.Startup))]
namespace MEAS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
