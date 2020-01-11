namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.Alarms", "Notes", c => c.String());
            AlterColumn("dbo.Rooms", "Floor", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Floor", c => c.Int(nullable: false));
            DropColumn("dbo.Alarms", "Notes");
            DropColumn("dbo.Alarms", "RoomId");
        }
    }
}
