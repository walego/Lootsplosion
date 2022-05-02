using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lootsplosion.MVC.Startup))]
namespace Lootsplosion.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
