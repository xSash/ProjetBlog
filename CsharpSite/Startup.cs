using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CsharpSite.Startup))]
namespace CsharpSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
