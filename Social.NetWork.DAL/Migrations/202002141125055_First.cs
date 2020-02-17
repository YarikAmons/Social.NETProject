namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserFriend", "FriendID", "dbo.Friends");
            DropIndex("dbo.UserFriend", new[] { "FriendID" });
            DropPrimaryKey("dbo.Friends");
            DropPrimaryKey("dbo.UserFriend");
            AlterColumn("dbo.Friends", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserFriend", "FriendID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Friends", "Id");
            AddPrimaryKey("dbo.UserFriend", new[] { "FriendID", "UserID" });
            CreateIndex("dbo.UserFriend", "FriendID");
            AddForeignKey("dbo.UserFriend", "FriendID", "dbo.Friends", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFriend", "FriendID", "dbo.Friends");
            DropIndex("dbo.UserFriend", new[] { "FriendID" });
            DropPrimaryKey("dbo.UserFriend");
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.UserFriend", "FriendID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Friends", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserFriend", new[] { "FriendID", "UserID" });
            AddPrimaryKey("dbo.Friends", "Id");
            CreateIndex("dbo.UserFriend", "FriendID");
            AddForeignKey("dbo.UserFriend", "FriendID", "dbo.Friends", "Id", cascadeDelete: true);
        }
    }
}
