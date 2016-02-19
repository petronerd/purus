using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PURUSInsurance.Startup))]
namespace PURUSInsurance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
