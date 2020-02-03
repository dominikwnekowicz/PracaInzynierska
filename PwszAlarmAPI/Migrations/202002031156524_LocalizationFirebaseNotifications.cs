namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalizationFirebaseNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SendNotifications", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SendNotifications");
        }
    }
}
