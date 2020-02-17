using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social.NetWork.DAL.Entities {
    public class UserProfile {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AboutMe { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Friend> Friends{ get; set; }
        public UserProfile() {
            Friends = new List<Friend>();
        }

    }
}
