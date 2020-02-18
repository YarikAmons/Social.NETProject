using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social.NetWork.DAL.Entities {
    public class Message {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FriendID { get; set; }
        public string UserID { get; set; }
        public string Envelope { get; set; }
        public string Date { get; set; }
        public string FullName { get; set; }
        public byte[] UserPhoto { get; set; }
    }
}
