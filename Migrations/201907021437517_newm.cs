namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserRides", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserRides", "Ride_RideID", "dbo.Rides");
            DropIndex("dbo.ApplicationUserRides", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRides", new[] { "Ride_RideID" });
            DropTable("dbo.ApplicationUserRides");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserRides",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Ride_RideID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Ride_RideID });
            
            CreateIndex("dbo.ApplicationUserRides", "Ride_RideID");
            CreateIndex("dbo.ApplicationUserRides", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserRides", "Ride_RideID", "dbo.Rides", "RideID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserRides", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
