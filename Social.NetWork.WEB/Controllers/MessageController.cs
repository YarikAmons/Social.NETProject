using Social.NetWork.BLL.DTO;
using Social.NetWork.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Controllers {
    public class MessageController : Controller {
        private readonly IUserService userService;
        private readonly IMessageService messageService;
        public MessageController(IMessageService _messageService, IUserService _userService) {
            userService = _userService;
            messageService = _messageService;
        }
        public async Task<ActionResult> Index(string UserID) {
            List<MessageDTO> model = await messageService.GetDialogs(UserID);
            model.Reverse();
            
            return View("Index", model);
        }
        public async Task<ActionResult> ToWriteAMessage(string UserID, string FriendID) {
            MessageDTO model = await messageService.CreateDialog(UserID, FriendID);
            List<MessageDTO> dialog = await messageService.GetMessagesWithThisFriend(UserID, FriendID);
            return View("Dialog", dialog);
        }
       
        
        
        public async Task<ActionResult> SendMessage(string UserID,string FriendID,string Envelope) {
            List<MessageDTO> dialog;
            try {
                await messageService.sendMessage(UserID, FriendID, Envelope);
                dialog = await messageService.GetMessagesWithThisFriend(UserID, FriendID); 
                //System.Threading.Thread.Sleep(2000);
                if (dialog.Last().Envelope == null || dialog.Last().Envelope.Length == 0) {
                    throw new Exception();
                }
            } catch (Exception ex ){
                return null;
            }
            return PartialView("SendMessagePartial",dialog);
        }
    }
}