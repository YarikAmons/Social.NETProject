using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Entities {
    class Dialog {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FriendID { get; set; }
        public string UserID { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public Dialog() {
            Messages = new List<Message>();
        }
    }
}
