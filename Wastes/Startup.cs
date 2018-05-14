using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wastes.Startup))]
namespace Wastes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
