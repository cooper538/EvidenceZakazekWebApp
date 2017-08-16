using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EvidenceZakazekWebApp.Startup))]
namespace EvidenceZakazekWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
