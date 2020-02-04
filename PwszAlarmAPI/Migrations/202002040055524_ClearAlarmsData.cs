namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearAlarmsData : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Alarms");
            Sql("DELETE FROM Messages");
        }
        
        public override void Down()
        {
        }
    }
}
