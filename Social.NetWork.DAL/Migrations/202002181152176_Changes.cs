namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.UserProfiles", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Chat_Id" });
            AddColumn("dbo.Messages", "FullName", c => c.String());
            AddColumn("dbo.Messages", "UserPhoto", c => c.Binary());
            DropColumn("dbo.Messages", "Chat_Id");
            DropColumn("dbo.UserProfiles", "Chat_Id");
            DropTable("dbo.Chats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserProfiles", "Chat_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Messages", "Chat_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Messages", "UserPhoto");
            DropColumn("dbo.Messages", "FullName");
            CreateIndex("dbo.UserProfiles", "Chat_Id");
            CreateIndex("dbo.Messages", "Chat_Id");
            AddForeignKey("dbo.UserProfiles", "Chat_Id", "dbo.Chats", "Id");
            AddForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
