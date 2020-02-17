using AutoMapper;
using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Infrastructures;
using Social.NetWork.BLL.Interfaces;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Services {
    public class FriendService : IFriendService {
        private readonly IUnitOfWork Database;
        private readonly IMapper Mapper;
        public FriendService(IUnitOfWork unitOfWork,IMapper mapper) {
            Database = unitOfWork;
            Mapper = mapper;
        }

        public async Task<OperationDetails> AddFriend(string Name,string currentIdUser) {
            UserProfile user = await Database.ClientManager.GetByName(Name);
            UserProfile mainUser = await Database.ClientManager.GetById(currentIdUser);
            if (user == null) {
                return null;
            }
            UserDTO userDTO = Mapper.Map<UserProfile, UserDTO>(user);
            try {
                Friend friend = Mapper.Map<UserDTO, Friend>(userDTO);
                friend.Name = userDTO.UserName;
                friend.UserProfiles.Add(mainUser);
                Database.FriendManager.Create(friend);
                await Database.SaveAsync();
            }catch(Exception ex) {
                return new OperationDetails(false, ex.Message, "");
            }
            return new OperationDetails(true, "Добавление успешно.", "");
        }
        public async Task<OperationDetails> DeleteFromFriend(string Name, string currentIdUser) {
            UserProfile user = await Database.ClientManager.GetByName(Name);
            UserProfile mainUser = await Database.ClientManager.GetById(currentIdUser);
            if (user == null||mainUser==null) {
                return null;
            }
            try {
                UserDTO userDTO = Mapper.Map<UserProfile, UserDTO>(user);
                Friend friend = Mapper.Map<UserDTO, Friend>(userDTO);
                friend.Name = userDTO.UserName;
                IQueryable<Friend> friend1 = Database.FriendManager.GetAll();
                int userFriendForDeleting;
                foreach (var el in friend1) {
                    if (el.IdFriend == friend.IdFriend) {
                        userFriendForDeleting = el.Id;
                        friend.Id = userFriendForDeleting;
                    }
                }
                Database.FriendManager.Delete(await Database.FriendManager.GetById(friend.Id));
                await Database.SaveAsync();
            } catch (Exception ex) {
                return new OperationDetails(false, ex.Message, "");
            }
            return new OperationDetails(true, "Удаление успешно.", "");
        }
        public List<FriendDTO> SearchingFriends(string name) {
            List<FriendDTO> friends = new List<FriendDTO>();
            List<Friend> result=null;
            if (name == "" || name == null) {
                result = Database.FriendManager.GetAll().ToList();
            } else {
                  result = Database.FriendManager.GetAll().Where(
                      s => s.Name.Contains(name)
                      ||s.Surname.Contains(name)
                      ||s.Name+" "+s.Surname==name
                      || s.Surname + " " + s.Name == name).ToList();
            }
            if (result != null) {
                foreach (var el in result) {
                    friends.Add(Mapper.Map<Friend,FriendDTO>(el));
                }
                return friends;
            } else {
                return null;
            }
            
            
        }
        public async Task<List<FriendDTO>> GetAll(string userId) {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            UserProfile profile = await Database.ClientManager.GetById(user.Id);
            if (user == null || profile == null)
                return null;
            UserDTO accountData = Mapper.Map<ApplicationUser, UserDTO>(user);
            List<Friend> list=new List<Friend>();
            IEnumerable<Friend> friend = Database.FriendManager.GetAll();
            foreach (var el in friend) {
                list.Add(el);
            }
            List<FriendDTO> friends = new List<FriendDTO>();
            for (int i = 0; i < list.Count; i++) {
                    UserProfile[] elem = list[i].UserProfiles.ToArray();
                if (elem.Length != 0) {
                    var second = elem[0].Id;
                    if (second == profile.Id) {
                        friends.Add(Mapper.Map<Friend, FriendDTO>(list[i]));
                    }
                }
            }
            
            return friends;
        }
        public Task<OperationDetails> GetById(FriendDTO friendDTO) {
            throw new NotImplementedException();
        }
        public void Dispose() => Database.Dispose();

    }
}
