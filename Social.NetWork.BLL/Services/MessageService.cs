﻿using AutoMapper;
using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Interfaces;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Services {
    public class MessageService:IMessageService {
        private readonly IUnitOfWork Database;
        private readonly IUserService userService;
        private readonly IFriendService friendService;
        private readonly IMapper Mapper;
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper,
            IUserService _userService,IFriendService _friendService) {
            Database = unitOfWork;
            userService = _userService;
            friendService = _friendService;
            Mapper = mapper;
        }
        
        static void SendMessageFromSocket(int port,string envelope) {
            
            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);
            string message=envelope;
            byte[] msg = Encoding.UTF8.GetBytes(message);
            int bytesSent = sender.Send(msg);
            int bytesRec = sender.Receive(bytes);
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        public async Task<List<MessageDTO>> GetMessagesWithThisFriend(string CurrentUserID, string FriendID) {
           ApplicationUser userProfile = await Database.UserManager.FindByIdAsync(CurrentUserID);
            if (userProfile == null)
                return null;
            List<Message> messWithThisFriend = new List<Message>();
            messWithThisFriend = Database.MessageManager.GetAll().ToList();
            List<MessageDTO> result = new List<MessageDTO>();
            foreach (var el in messWithThisFriend) {
                if (el.UserID == CurrentUserID && el.FriendID == FriendID || el.UserID == FriendID && el.FriendID == CurrentUserID) {
                    if (!result.Contains(Mapper.Map<Message, MessageDTO>(el))) {
                        result.Add(Mapper.Map<Message, MessageDTO>(el));
                    }
                }
            }
            return result;
        }
        public MessageDTO GetSingleDialog(string CurrentUserID, string FriendID) {
            MessageDTO result = new MessageDTO();
            List<Message> elem=Database.MessageManager.GetAll().Where(p => p.UserID == CurrentUserID && p.FriendID == FriendID).ToList();
            Message single=elem[0];
            result = Mapper.Map<Message,MessageDTO>(single);
            return result;
        }
        public async Task<List<MessageDTO>> GetDialogs(string CurrentUserID) {
            ApplicationUser userProfile = await Database.UserManager.FindByIdAsync(CurrentUserID);
            if(userProfile==null)
                return null;
            else {
                List<Message> messCurrentUser = new List<Message>();
                messCurrentUser = Database.MessageManager.GetAll().Where(r=>r.UserID==CurrentUserID).ToList();
                List<MessageDTO> results = new List<MessageDTO>();
                foreach(var el in messCurrentUser) {
                    if (!results.Contains(Mapper.Map<Message, MessageDTO>(el))) {
                        results.Add(Mapper.Map<Message, MessageDTO>(el));
                    }
                }
                return results;
            }
        }
        public async Task<MessageDTO> CreateDialog(string CurrentUserID, string FriendID) {
            UserProfile userProfile = await Database.ClientManager.GetById(CurrentUserID);
            UserProfile friendProfile = await Database.ClientManager.GetById(FriendID);
            if (userProfile == null || friendProfile == null) {
                return null;
            } else {
                IEnumerable<Message> list =Database.MessageManager.GetAll();
                foreach(var el in list) {
                    if (el.FriendID == friendProfile.Id && el.UserID == userProfile.Id) {
                        return null;
                    }
                }
                Message letters = new Message();
                letters.UserID = userProfile.Id;
                letters.FullName = friendProfile.UserName + " " + friendProfile.Surname;
                letters.UserPhoto = friendProfile.UserPhoto;
                letters.FriendID = friendProfile.Id;
                letters.Date = DateTime.Now.ToString("HH:mm:ss");
                try {
                    Database.MessageManager.Create(letters);
                    await Database.SaveAsync();
                    return Mapper.Map<Message,MessageDTO>(letters);
                } catch(Exception ex) {
                    return null;
                }
            }
        }
        public async Task<MessageDTO> sendMessage(string UserID, string FriendID, string Envelope) {
            UserProfile userProfile = await Database.ClientManager.GetById(FriendID);
            Message message = new Message();
            if (Envelope.Length!= 0) {
                message.FullName = userProfile.UserName + " " + userProfile.Surname;
                message.UserPhoto = userProfile.UserPhoto;
                message.UserID = UserID;
                message.FriendID = FriendID;
                message.Envelope = Envelope;
                message.Date = DateTime.Now.ToString("HH:mm:ss");
            }
            try {
                if (Envelope.Length!=0) {
                    Database.MessageManager.Create(message);
                    await Database.SaveAsync();
                   
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                return null;
            }
            MessageDTO result = Mapper.Map<Message, MessageDTO>(message);
            return result;
        }
        public void Dispose() =>Database.Dispose();

       
    }
}
