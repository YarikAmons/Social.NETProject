using Microsoft.AspNet.Identity.EntityFramework;

namespace Social.NetWork.DAL.Entities {
    public class ApplicationUser:IdentityUser {
        public virtual UserProfile UserProfile { get; set; }
      
    }
   
}
