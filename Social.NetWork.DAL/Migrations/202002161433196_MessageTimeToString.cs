namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageTimeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Date", c => c.DateTime());
        }
    }
}
