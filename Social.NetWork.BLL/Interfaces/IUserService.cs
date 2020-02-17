using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Interfaces {
    public interface IUserService :IDisposable{
        Task<UserDTO> GetUserInfo(string userId);
        Task<OperationDetails> Create(UserDTO userDto);

        Task<List<UserDTO>> GetAllPeople(string userID);
        List<UserDTO> SearchingPeople(string Name);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task<OperationDetails> SetInitialData(UserDTO adminDTO, List<string> roles);
    }
}
