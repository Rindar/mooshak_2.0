using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mooshak_2._0.Startup))]
namespace mooshak_2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
