using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsurerWebApplication.Startup))]
namespace InsurerWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
