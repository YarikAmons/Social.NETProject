using Microsoft.Owin.Security;
using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Controllers {
    public class PeopleController :Controller{
        private readonly IUserService userService;
        private readonly IFriendService friendService;
        private readonly IAuthenticationManager authManager;
        public PeopleController(IUserService _userService, IFriendService _friendService,
            IAuthenticationManager authenticationManager) {
            userService = _userService;
            friendService = _friendService;
            authManager = authenticationManager;
        }
        public async Task<ActionResult> Index(string userId) {
            List<UserDTO> ListOfPeople = await userService.GetAllPeople(userId);
            return PartialView(ListOfPeople);
        }

        [HttpPost]
        public ActionResult PeopleSearch(string name) {
            var allpeople = userService.SearchingPeople(name);
            if (allpeople == null) {
                return HttpNotFound();
            }
            return PartialView("_PeoplePartial",allpeople);
        }
        public async Task<ActionResult> AddToFriends(string Name, string userId) {
           await friendService.AddFriend(Name, userId);
           List<UserDTO> ListOfPeople = await userService.GetAllPeople(userId);
           return PartialView("Index",ListOfPeople);
        }
        

        
    }
}