namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillingRoomsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Side", c => c.String());

            Sql("DELETE FROM Rooms");
            //
            //Ground Floor
            //
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.1', 'Parter', 'main')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.2', 'Parter', 'main')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.3', 'Parter', 'main')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sklepik', 'Parter', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.4', 'Parter', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.5', 'Parter', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.6', 'Parter', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.7', 'Parter', 'seven')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.8', 'Parter', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Męski', 'Parter', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Damski', 'Parter', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.9', 'Parter', 'main')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 0.10', 'Parter', 'main')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Portiernia', 'Parter', 'main')");
            //                                                  
            //First Floor                                       
            //                                                  
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Dyrektor, Wicedyrektor', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sekretariat', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.1', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.2', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.3', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.4', 'Pierwsze piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.5', 'Pierwsze piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.6', 'Pierwsze piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.7', 'Pierwsze piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.8', 'Pierwsze piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.9', 'Pierwsze piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.10', 'Pierwsze piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.11', 'Pierwsze piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.12', 'Pierwsze piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.13', 'Pierwsze piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Męski', 'Pierwsze piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC', 'Pierwsze piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC dla Niepełnosprawnych', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Damski', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.14', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.15', 'Pierwsze piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 1.16', 'Pierwsze piętro', 'front')");
            //                                           
            //Second Floor
            //                                           
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.1', 'Drugie piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.2', 'Drugie piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.2A', 'Drugie piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.3', 'Drugie piętro', 'right')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.4', 'Drugie piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.5', 'Drugie piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.6', 'Drugie piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.7', 'Drugie piętro', 'back')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.8', 'Drugie piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.8A', 'Drugie piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Męski', 'Drugie piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC', 'Drugie piętro', 'left')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC dla niepełnosprawnych', 'Drugie piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('WC Damski', 'Drugie piętro', 'front')");
            Sql("INSERT INTO Rooms (Name, Floor, Side) VALUES ('Sala 2.9', 'Drugie piętro', 'front')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Side");
        }
    }
}
