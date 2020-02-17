namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Envelope", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Envelope", c => c.String());
        }
    }
}
