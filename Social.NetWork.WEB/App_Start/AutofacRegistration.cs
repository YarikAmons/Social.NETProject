using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.Owin.Security;
using Social.NetWork.BLL.Infrastructures;
using Social.NetWork.DAL.Infrastructure;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Social.NetWork.WEB.App_Start {
    public class AutofacRegistration {
        public static IContainer BuildContainer() {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly webAppAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(webAppAssembly);
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.Register(c => AutomapperConfiguration.Configure()).As<IMapper>().SingleInstance();

            builder.RegisterModule(new DomainAccessModule());
            builder.RegisterModule(new BusinessLogicModule());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}