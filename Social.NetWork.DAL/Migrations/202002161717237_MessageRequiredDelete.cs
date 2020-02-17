namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageRequiredDelete : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Envelope", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Envelope", c => c.String(nullable: false));
        }
    }
}
