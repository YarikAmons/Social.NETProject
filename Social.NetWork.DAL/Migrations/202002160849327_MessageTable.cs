namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendID = c.String(),
                        UserID = c.String(),
                        Envelope = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
