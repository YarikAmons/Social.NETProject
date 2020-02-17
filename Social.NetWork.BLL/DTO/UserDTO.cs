using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.DTO {
    public class UserDTO {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string AboutMe { get; set; }
        public byte[] UserPhoto { get; set; }
        public string Role { get; set; }
        public string FullName => $"{UserName} {Surname}";
        public string ContactPhone { get; set; }
        
    }
}
