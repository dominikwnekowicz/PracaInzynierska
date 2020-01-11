namespace PwszAlarmAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomsData : DbMigration
    {
            public override void Up()
            {
            Sql("DELETE FROM Rooms");
                //
                //Ground Floor
                //
            Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.1', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.2', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.3', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.4', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.5', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.6', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.7', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.8', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.9', 'Parter')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('0.10', 'Parter')");
                //                                                  
                //First Floor                                       
                //                                                  
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.1', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.2', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.3', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.4', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.5', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.6', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.7', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.8', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.9', 'Pierwsze piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('1.10', 'Pierwsze piêtro')");
                //                                           
                //Second Floor                               
                //                                           
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.1', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.2', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.3', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.4', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.5', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.6', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.7', 'Drugie piêtro')");
                Sql("INSERT INTO Rooms (Name, Floor) VALUES ('2.8', 'Drugie piêtro')");
            }

            public override void Down()
            {
                
            }
        }
    }