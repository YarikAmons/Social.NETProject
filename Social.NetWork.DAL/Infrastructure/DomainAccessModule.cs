using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Identity;
using Social.NetWork.DAL.Interfaces;
using Social.NetWork.DAL.Repositories;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Social.NetWork.DAL.Infrastructure {
    public class DomainAccessModule :Autofac.Module{
        protected override void Load(ContainerBuilder builder) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.RegisterType<ApplicationContext>().AsSelf().WithParameter("connectionString", connectionString).InstancePerRequest();

            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationContext>())).As<UserStore<ApplicationUser>>().InstancePerRequest();
            builder.Register(c => new RoleStore<ApplicationRole>(c.Resolve<ApplicationContext>())).As<RoleStore<ApplicationRole>>().InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            Assembly dataAccessAssembly = Assembly.GetAssembly(typeof(IUnitOfWork));
            builder.RegisterAssemblyTypes(dataAccessAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<ClientManager>().As<IClientManager>().InstancePerRequest();
            builder.RegisterType<FriendManager>().As<IFriendManager>().InstancePerRequest();
            builder.RegisterType<MessageManager>().As<IMessageManager>().InstancePerRequest();
            builder.RegisterType<UserFriendManager>().As<IUserFriend>().InstancePerRequest();
        }
    }
}
