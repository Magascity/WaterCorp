using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WaterCorp.Startup))]
namespace WaterCorp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
