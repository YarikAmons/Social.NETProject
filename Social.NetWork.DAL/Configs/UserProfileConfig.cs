using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Configs {
    public class UserProfileConfig : EntityTypeConfiguration<UserProfile>{
        public UserProfileConfig() {
            HasKey(p => p.Id);
            Property(p => p.UserName).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.Surname).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.UserPhoto).HasColumnType("image").IsOptional();
            Property(p => p.BirthDate).HasColumnType("date").IsOptional();
            Property(p => p.ContactEmail).HasColumnType("nvarchar").HasMaxLength(256);
            Property(p => p.ContactPhone).HasColumnType("nvarchar").HasMaxLength(15);
            Property(p => p.AboutMe).HasColumnType("ntext").IsOptional();

            HasRequired(p => p.ApplicationUser).WithRequiredDependent(p => p.UserProfile).WillCascadeOnDelete(true);
        }
    }
}
