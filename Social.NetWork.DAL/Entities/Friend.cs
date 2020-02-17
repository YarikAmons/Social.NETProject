using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social.NetWork.DAL.Entities {
    public class Friend {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserProfiles")]
        public string IdFriend { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] UserPhoto { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AboutMe { get; set; }
        public virtual ICollection<UserProfile> UserProfiles {get;set;}
        public Friend() {
                UserProfiles = new List<UserProfile>();
        }

    }
}
