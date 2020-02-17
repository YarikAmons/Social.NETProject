using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Controllers {
    public class FriendController :Controller{
        private readonly IFriendService friendService;
        private readonly IMessageService messageService;
        public FriendController(IFriendService _friendService,
            IMessageService _message) {
            friendService = _friendService;
            messageService = _message;
        }
        public async Task<ActionResult> FriendsList(string userId) {
            List<FriendDTO> listOfFriends = await friendService.GetAll(userId);
            return View(listOfFriends);
        }
        public async Task<ActionResult> DeleteFromFriends(string Name, string userId) {
            await friendService.DeleteFromFriend(Name, userId);
            List<FriendDTO> listOfFriends = await friendService.GetAll(userId);
            return View("FriendsList",listOfFriends);
        }
        


        [HttpPost]
        public ActionResult FriendSearch(string name) {
            var allfriends = friendService.SearchingFriends(name);
            if (allfriends == null) {
                return HttpNotFound();
            }
            return PartialView(allfriends);
        }

    }
}