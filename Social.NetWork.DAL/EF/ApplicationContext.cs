using Microsoft.AspNet.Identity.EntityFramework;
using Social.NetWork.DAL.Configs;
using Social.NetWork.DAL.Entities;
using System.Data.Entity;

namespace Social.NetWork.DAL.EF {
   
    public class ApplicationContext :IdentityDbContext{
        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public ApplicationContext() { }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new UserProfileConfig());
            modelBuilder.Entity<Friend>()
                .HasMany(c => c.UserProfiles)
                .WithMany(s => s.Friends)
                .Map(t => t.MapLeftKey("FriendID")
                .MapRightKey("UserID")
                .ToTable("UserFriend"));
            base.OnModelCreating(modelBuilder);
        }
      
        public void SetModified<T>(T item) => Entry(item).State = EntityState.Modified;

       
    }
}
