namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RideReservations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RideReservations", "Ride_RideID", "dbo.Rides");
            DropIndex("dbo.RideReservations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RideReservations", new[] { "Ride_RideID" });
            AddColumn("dbo.RideReservations", "RideID", c => c.Int(nullable: false));
            AddColumn("dbo.RideReservations", "PassengerID", c => c.String());
            AddColumn("dbo.RideReservations", "Seats", c => c.Int(nullable: false));
            DropColumn("dbo.RideReservations", "ApplicationUser_Id");
            DropColumn("dbo.RideReservations", "Ride_RideID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RideReservations", "Ride_RideID", c => c.Int());
            AddColumn("dbo.RideReservations", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.RideReservations", "Seats");
            DropColumn("dbo.RideReservations", "PassengerID");
            DropColumn("dbo.RideReservations", "RideID");
            CreateIndex("dbo.RideReservations", "Ride_RideID");
            CreateIndex("dbo.RideReservations", "ApplicationUser_Id");
            AddForeignKey("dbo.RideReservations", "Ride_RideID", "dbo.Rides", "RideID");
            AddForeignKey("dbo.RideReservations", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
