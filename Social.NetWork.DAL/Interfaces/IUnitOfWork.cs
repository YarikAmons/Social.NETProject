using Social.NetWork.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Interfaces {
    public interface IUnitOfWork :IDisposable{
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }
        IMessageManager MessageManager { get; }
        IFriendManager FriendManager { get; }
        Task SaveAsync();
    }
}
