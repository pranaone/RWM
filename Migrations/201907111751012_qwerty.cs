namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwerty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RideReservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        ReservationStatus = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Ride_RideID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Rides", t => t.Ride_RideID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Ride_RideID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RideReservations", "Ride_RideID", "dbo.Rides");
            DropForeignKey("dbo.RideReservations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RideReservations", new[] { "Ride_RideID" });
            DropIndex("dbo.RideReservations", new[] { "ApplicationUser_Id" });
            DropTable("dbo.RideReservations");
        }
    }
}
