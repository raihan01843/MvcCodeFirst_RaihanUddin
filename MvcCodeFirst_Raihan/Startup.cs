using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcCodeFirst_Raihan.Startup))]
namespace MvcCodeFirst_Raihan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
