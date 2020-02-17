using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.DTO {
    public class MessageDTO {
        public string Id { get; set; }
        public string FriendID { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public byte[] UserPhoto { get; set; }
        public string Envelope { get; set; }
        public string Date { get; set; }
    }
}
