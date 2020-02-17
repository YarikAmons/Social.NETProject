using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Social.NetWork.DAL.Entities;
using System.Configuration;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin;
using Social.NetWork.DAL.EF;
using System;

namespace Social.NetWork.DAL.Identity {
    public class ApplicationUserManager:UserManager<ApplicationUser> {
        public ApplicationUserManager(UserStore<ApplicationUser> store) 
            : base(store) 
            {
            
        }

    }
}
