namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AthenticationAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Level", c => c.Byte(nullable: false));
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "JoinDate");
            DropColumn("dbo.AspNetUsers", "Level");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
