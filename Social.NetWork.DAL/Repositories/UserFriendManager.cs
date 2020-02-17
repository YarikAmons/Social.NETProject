using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Repositories {
    public class UserFriendManager : Repository<UserFriend>, IUserFriend {
        public UserFriendManager(ApplicationContext context) : base(context) { }
    }
}
