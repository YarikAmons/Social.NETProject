using AutoMapper;
using Microsoft.Owin.Security;
using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Infrastructures;
using Social.NetWork.BLL.Interfaces;
using Social.NetWork.WEB.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Controllers {
    public class AccountController : Controller{
        private readonly IUserService userService;
        private readonly IFriendService friendService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IAuthenticationManager authManager;
        public AccountController(IUserService _userService,IMapper _mapper,IFriendService _friendService,IImageService _imageService,
            IAuthenticationManager authenticationManager) {
            userService = _userService;
            imageService = _imageService;
            friendService = _friendService;
            mapper = _mapper;
            authManager = authenticationManager;
        }
        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model) {
            if (ModelState.IsValid) {
                UserDTO userDto = mapper.Map<UserDTO>(model);
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                if (claim == null) {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                } else {
                    authManager.SignOut();
                    authManager.SignIn(new AuthenticationProperties {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home", userDto);
                }
            }
            return View(model);
        }
        public ActionResult Logout() {
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationModel model) {
            await SetInitialDataAsync();
            if (ModelState.IsValid) {
                UserDTO userDTO = mapper.Map<UserDTO>(model);
                userDTO.UserPhoto = await GetDefaultProfileImage();
                OperationDetails operationDetails = await userService.Create(userDTO);
                if (operationDetails.Succeeded){
                    ClaimsIdentity claim = await userService.Authenticate(userDTO);
                    authManager.SignOut();
                    UserProfileModel userProfileModel = mapper.Map<UserDTO, UserProfileModel>(userDTO);
                    authManager.SignIn(new AuthenticationProperties {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home",userDTO);
                } else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        
        public async Task<byte[]> GetDefaultProfileImage() {
            string path = ConfigurationManager.AppSettings["defaultProfilePicture"];
            string fullPath = Server.MapPath($"~/Content/{path}");
            byte[] defaultProfileImage = await imageService.GetImageDataFromFS(fullPath);
            return defaultProfileImage;
        }
        private async Task SetInitialDataAsync() {
            await userService.SetInitialData(new UserDTO {
                Email = "yarysyk51@gmail.com",
                AboutMe = "",
                UserPhoto = await GetDefaultProfileImage(),
                Password = "Nastya13542",
                UserName = "Ярослав",
                Surname="Амонс",
                Role = "admin",
            }, new List<string> { "user", "admin" }) ;
        }
        public async Task<ActionResult> GetAllMyInfo(string CurrentUserID) {
            var result = await userService.GetUserInfo(CurrentUserID);
            return View("GetAllMyInfo", result);
        }
    }
}