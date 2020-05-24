using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ridewithme.Startup))]
namespace Ridewithme
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
