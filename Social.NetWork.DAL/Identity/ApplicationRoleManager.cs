using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Identity {
        public class ApplicationRoleManager : RoleManager<ApplicationRole> {
            public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store) { }
        }
    }

