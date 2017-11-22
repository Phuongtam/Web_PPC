using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPC.Startup))]
namespace PPC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
