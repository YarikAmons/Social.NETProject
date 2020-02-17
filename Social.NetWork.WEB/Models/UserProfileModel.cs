using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Social.NetWork.WEB.Models {
    public class UserProfileModel {
        public string Id { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AboutMe { get; set; }
    }
}