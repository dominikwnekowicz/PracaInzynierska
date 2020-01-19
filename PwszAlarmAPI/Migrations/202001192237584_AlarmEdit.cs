namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "UserId", c => c.String());
            DropColumn("dbo.Alarms", "UserEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alarms", "UserEmail", c => c.String());
            DropColumn("dbo.Alarms", "UserId");
        }
    }
}
