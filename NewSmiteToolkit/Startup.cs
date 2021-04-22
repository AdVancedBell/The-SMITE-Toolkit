using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewSmiteToolkit.Startup))]
namespace NewSmiteToolkit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
