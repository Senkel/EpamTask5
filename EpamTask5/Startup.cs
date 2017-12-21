using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EpamTask5.Startup))]
namespace EpamTask5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
