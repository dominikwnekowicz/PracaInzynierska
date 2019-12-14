using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PwszAlarmAPI.Startup))]
namespace PwszAlarmAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
