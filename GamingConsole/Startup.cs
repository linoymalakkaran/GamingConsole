using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GamingConsole.Startup))]
namespace GamingConsole
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}