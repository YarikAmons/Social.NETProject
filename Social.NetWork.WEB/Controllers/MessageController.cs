using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Controllers {
    public class MessageController :Controller {
        private readonly IUserService userService;
        private readonly IMessageService messageService;
        public MessageController(IMessageService _messageService,IUserService _userService) {
            userService = _userService;
            messageService = _messageService;
        }

        public async Task<ActionResult> Index(string UserID) {
            List<MessageDTO> model = await messageService.GetDialogs(UserID);
            return View("Index",model);
        }
        public async Task<ActionResult> ToWriteAMessage(string UserID, string FriendID) {
            MessageDTO model = await messageService.CreateDialog(UserID, FriendID);
            List<MessageDTO> dialog = await messageService.GetMessagesWithThisFriend(UserID, FriendID);
            return View("Dialog",dialog);
        }
        public async Task<ActionResult> SendMessage(string UserID,string FriendID,string Envelope) {
            
            Response.Write("<script type='text/javascript'> setTimeout('location.reload(true);',5000);</script>");
            List<MessageDTO> dialog =new List<MessageDTO>();
            try {
                await messageService.sendMessage(UserID, FriendID, Envelope);
                dialog = await messageService.GetMessagesWithThisFriend(UserID, FriendID);
                if (dialog.Last().Envelope == null || dialog.Last().Envelope.Length == 0) {
                    throw new Exception();
                }
            } catch (Exception ex ){
                return null;
            }
            return PartialView("SendMessagePartial",dialog);

            
        }
        // Response.AddHeader("Refresh", "10");d
    }
}