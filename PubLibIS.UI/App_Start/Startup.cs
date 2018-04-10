using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PubLibIS.BLL.Services;
using PubLibIS.UI.Util;

[assembly: OwinStartup(typeof(PubLibIS.UI.App_Start.Startup))]

namespace PubLibIS.UI.App_Start
{
    public class Startup
    {
        ServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private UserService CreateUserService()
        {
            return serviceCreator.CreateUserService(ConnectionStringResolver.CurrentConnectionString);
        }
    }
}