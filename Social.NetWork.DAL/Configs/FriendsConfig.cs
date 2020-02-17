using Social.NetWork.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Social.NetWork.DAL.Configs {
    public class FriendsConfig :EntityTypeConfiguration<Friend>{
        public FriendsConfig() {
            HasKey(p => p.Id);
            Property(p => p.IdFriend).HasColumnType("nvarchar");
            Property(p => p.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.Surname).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.UserPhoto).HasColumnType("image").IsOptional();
            Property(p => p.BirthDate).HasColumnType("date").IsOptional();
            Property(p => p.ContactEmail).HasColumnType("nvarchar").HasMaxLength(256);
            Property(p => p.ContactPhone).HasColumnType("nvarchar").HasMaxLength(15);
            Property(p => p.AboutMe).HasColumnType("ntext").IsOptional();
        }

    }
}
