using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPrueba.Startup))]
namespace MVCPrueba
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
