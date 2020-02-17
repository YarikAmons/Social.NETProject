using Social.NetWork.BLL.DTO;
using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Interfaces {
    public interface IMessageService :IDisposable {
        Task<MessageDTO> CreateDialog(string CurrecntUserID, string FriendID);
        Task<List<MessageDTO>> GetMessagesWithThisFriend(string CurrentUserID,string FriendID);
        Task<List<MessageDTO>> GetDialogs(string CurrentUserID);
        Task<MessageDTO> sendMessage(string UserID, string FriendID, string Envelope);
        MessageDTO GetSingleDialog(string UserID, string FriendID);

    }
}
