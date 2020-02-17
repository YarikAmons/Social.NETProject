using Social.NetWork.DAL.Entities;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Interfaces {
    public interface IClientManager : IRepository<UserProfile>{
        Task<UserProfile> GetByName(string Name);
    }
}
