using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Infrastructures;
using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Interfaces {
    public interface IFriendService :IDisposable {
        Task<List<FriendDTO>> GetAll(string userID);
        Task<OperationDetails> AddFriend(string Name, string currentUser);
        List<FriendDTO> SearchingFriends(string Name);
        Task<OperationDetails> DeleteFromFriend(string Name, string currentUser);
        Task<OperationDetails> GetById(FriendDTO friendDTO);



    }
}
