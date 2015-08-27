using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(RhastyGaming.Startup))]
namespace RhastyGaming
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
