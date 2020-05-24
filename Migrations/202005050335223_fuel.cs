namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fuels",
                c => new
                    {
                        FuelID = c.Int(nullable: false, identity: true),
                        FuelName = c.String(),
                        FuelPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FuelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fuels");
        }
    }
}
