namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmsTableClear : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Alarms");

            AddColumn("dbo.Alarms", "Name", c => c.String());
            AddColumn("dbo.Alarms", "Archived", c => c.Boolean(nullable: false));
            DropColumn("dbo.Alarms", "Notes");
            DropColumn("dbo.Alarms", "Accepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alarms", "Accepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Alarms", "Notes", c => c.String());
            DropColumn("dbo.Alarms", "Archived");
            DropColumn("dbo.Alarms", "Name");
        }
    }
}
