using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Repositories {
    public class FriendManager : Repository<Friend>,IFriendManager{
        public FriendManager(ApplicationContext context):base(context){ }
        
    }
}
