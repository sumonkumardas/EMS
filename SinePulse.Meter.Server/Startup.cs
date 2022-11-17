using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SinePulse.SmartMeter.Server.Startup))]
namespace SinePulse.SmartMeter.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
