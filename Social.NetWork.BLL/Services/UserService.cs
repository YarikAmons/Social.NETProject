using AutoMapper;
using Microsoft.AspNet.Identity;
using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Infrastructures;
using Social.NetWork.BLL.Interfaces;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Services {
    public class UserService : IUserService {
        private readonly IUnitOfWork Database;
        private readonly IMapper Mapper;
        public UserService(IUnitOfWork uow,IMapper mapper) {
            Database = uow;
            Mapper = mapper;
        }


        public async Task<OperationDetails> Create(UserDTO userDto) {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null) {
                user = new ApplicationUser();
                user = Mapper.Map<UserDTO, ApplicationUser>(userDto,user);
                IdentityResult userCreationResult = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (userCreationResult.Errors.Count() > 0) {
                    return new OperationDetails(false, userCreationResult.Errors.FirstOrDefault(), "Ошибка создания учётной записи");
                }
                var userRolSetResult = await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                if (userRolSetResult.Errors.Count() > 0)
                    return new OperationDetails(false, userRolSetResult.Errors.FirstOrDefault(), "");
                try {
                    UserProfile userProfile = Mapper.Map<UserDTO, UserProfile>(userDto);
                    userProfile.Id = user.Id;
                    userProfile.ContactEmail = user.Email;
                    Database.ClientManager.Create(userProfile);
                    await Database.SaveAsync();
                } catch(DbUpdateException ex) {
                    return new OperationDetails(false,ex.Message, "");
                }
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            } else {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto) {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        public async Task<OperationDetails> SetInitialData(UserDTO adminDTO, List<string> roles) {
            foreach (string roleName in roles) {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null) {

                    role = new ApplicationRole { Name = roleName };
                    var roleCreationResult = await Database.RoleManager.CreateAsync(role);
                    if (roleCreationResult.Errors.Count() > 0) 
                        return new OperationDetails(false, roleCreationResult.Errors.FirstOrDefault(), "");
                }
            }
            await Create(adminDTO);
            return new OperationDetails(true, "Инициализация успешно завершена.","");
        }
        public async Task<List<UserDTO>> GetAllPeople(string userId) {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            UserProfile profile = await Database.ClientManager.GetById(user.Id);
            if (user == null || profile == null)
                return null;
            List<UserDTO> list = new List<UserDTO>();
            IQueryable<UserProfile> userList = Database.ClientManager.GetAll();
            foreach (var el in userList) {
                if (el.Id != profile.Id) {
                    list.Add(Mapper.Map<UserProfile, UserDTO>(el));
                }
            }
            return list;
        }
        public List<UserDTO> SearchingPeople(string name) {
            List<UserDTO> people = new List<UserDTO>();
            List<UserProfile> result = null;
            if (name == "" || name == null) {
                result = Database.ClientManager.GetAll().ToList();
            } else {
                result = Database.ClientManager.GetAll().Where(
                    s => s.UserName.Contains(name)
                    || s.Surname.Contains(name)
                    || s.UserName + " " + s.Surname == name
                    || s.Surname + " " + s.UserName == name).ToList();
            }
            if (result != null) {
                foreach (var el in result) {
                    people.Add(Mapper.Map<UserProfile, UserDTO>(el));
                }
                return people;
            } else {
                return null;
            }


        }
        public async Task<UserDTO> GetUserInfo(string userId) {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            UserProfile profile = await Database.ClientManager.GetById(user.Id);

            if (user == null || profile == null)
                return null;

            UserDTO accountData = Mapper.Map<ApplicationUser, UserDTO>(user);
            accountData.Role = user != null ? Database.UserManager.GetRoles(user.Id).FirstOrDefault() : null;
            accountData = Mapper.Map<UserProfile, UserDTO>(profile, accountData);

            return accountData;
        }
        public void Dispose() => Database.Dispose();

    }
}
