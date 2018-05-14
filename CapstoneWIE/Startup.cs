using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapstoneWIE.Startup))]
namespace CapstoneWIE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
