namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RideReservations", "DriverName", c => c.String());
            AddColumn("dbo.RideReservations", "PassengerName", c => c.String());
            AddColumn("dbo.RideReservations", "SeatsRequired", c => c.Int(nullable: false));
            DropColumn("dbo.RideReservations", "PassengerID");
            DropColumn("dbo.RideReservations", "Seats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RideReservations", "Seats", c => c.Int(nullable: false));
            AddColumn("dbo.RideReservations", "PassengerID", c => c.String());
            DropColumn("dbo.RideReservations", "SeatsRequired");
            DropColumn("dbo.RideReservations", "PassengerName");
            DropColumn("dbo.RideReservations", "DriverName");
        }
    }
}
