using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PubLibIS.BLL.Interfaces;
using PubLibIS.BLL.Services;
using PubLibIS.View.Util;

[assembly: OwinStartup(typeof(PubLibIS.View.App_Start.Startup))]

namespace PubLibIS.View.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService(ConnectionStringResolver.CurrentConnectionString);
        }
    }
}