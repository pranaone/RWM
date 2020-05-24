namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rides",
                c => new
                    {
                        RideID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Origin = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        Schedule = c.Int(nullable: false),
                        Occurance = c.Int(nullable: false),
                        VehicleType = c.Int(nullable: false),
                        Model = c.String(),
                        Color = c.String(),
                        AirConditioned = c.Int(nullable: false),
                        AvailableSeats = c.Int(nullable: false),
                        Contribution = c.String(),
                        Message = c.String(),
                        IsNonSmoking = c.Boolean(nullable: false),
                        IsMaleOnly = c.Boolean(nullable: false),
                        IsFemaleOnly = c.Boolean(nullable: false),
                        DateAdded = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.RideID);
            
            CreateTable(
                "dbo.ApplicationUserRides",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Ride_RideID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Ride_RideID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.Ride_RideID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Ride_RideID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRides", "Ride_RideID", "dbo.Rides");
            DropForeignKey("dbo.ApplicationUserRides", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserRides", new[] { "Ride_RideID" });
            DropIndex("dbo.ApplicationUserRides", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserRides");
            DropTable("dbo.Rides");
        }
    }
}
