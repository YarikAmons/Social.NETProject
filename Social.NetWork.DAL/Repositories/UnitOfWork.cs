using Microsoft.AspNet.Identity.EntityFramework;
using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Identity;
using Social.NetWork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationContext _context;
        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public IClientManager ClientManager { get; }
        public IFriendManager FriendManager { get; }
        public IUserFriend UserFriendManager { get; }
        public IMessageManager MessageManager { get; }
        public UnitOfWork(ApplicationContext context,
            ApplicationUserManager _userManager,
            ApplicationRoleManager _roleManager,
            IClientManager _clientManager,
            IFriendManager friendManager,
            IMessageManager messageManager,
            IUserFriend _userFriend) 
            {
            MessageManager = messageManager;
            UserFriendManager = _userFriend;
            _context = context;
            UserManager = _userManager;
            RoleManager = _roleManager;
            ClientManager = _clientManager;
            FriendManager = friendManager;
        }
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
