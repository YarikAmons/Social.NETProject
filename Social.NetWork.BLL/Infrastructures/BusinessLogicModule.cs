using Social.NetWork.BLL.Interfaces;
using System.Reflection;
using Autofac;

namespace Social.NetWork.BLL.Infrastructures {
    public class BusinessLogicModule :Autofac.Module{
        protected override void Load(ContainerBuilder builder) {
            Assembly businessLogicAssembly = Assembly.GetAssembly(typeof(IUserService));
            builder.RegisterAssemblyTypes(businessLogicAssembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
        }
    }
}
