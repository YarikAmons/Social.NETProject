namespace Social.NetWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Date", c => c.DateTime(nullable: false));
        }
    }
}
