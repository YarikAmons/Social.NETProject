using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Repositories {
    public class ClientManager : Repository<UserProfile>,IClientManager {
        public ClientManager(ApplicationContext context) : base(context) { }

        public virtual async Task<UserProfile> GetByName(string Name) {
            IQueryable<UserProfile> items = GetAll();

            if (!string.IsNullOrWhiteSpace(Name))
                items = items.Where(e => e.UserName.StartsWith(Name));

            return items.FirstOrDefault();
        }
    }
}
