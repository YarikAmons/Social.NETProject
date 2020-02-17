using Social.NetWork.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Interfaces {
    public interface IUserFriendService :IDisposable{
        Task<List<UserFriendDTO>> GetAll();
    }
}
