namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FullName", c => c.String());
            AddColumn("dbo.Messages", "UserPhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "UserPhoto");
            DropColumn("dbo.Messages", "FullName");
        }
    }
}
