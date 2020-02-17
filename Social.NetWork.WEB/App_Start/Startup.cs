using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Social.NetWork.DAL.Identity;

[assembly: OwinStartup(typeof(Social.NetWork.WEB.App_Start.Startup))]

namespace Social.NetWork.WEB.App_Start {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Account/Login")
            });
            app.UseAutofacMiddleware(AutofacRegistration.BuildContainer());
           
        }
    }
}