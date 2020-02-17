using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;

namespace Social.NetWork.DAL.Repositories {
    public class MessageManager:Repository<Message>,IMessageManager {
        public MessageManager(ApplicationContext context) : base(context) { }
    }
}
