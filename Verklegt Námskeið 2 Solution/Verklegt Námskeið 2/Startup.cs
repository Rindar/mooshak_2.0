using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Verklegt_Námskeið_2.Startup))]
namespace Verklegt_Námskeið_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
