namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LevelDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Level", c => c.Byte(nullable: false));
        }
    }
}
