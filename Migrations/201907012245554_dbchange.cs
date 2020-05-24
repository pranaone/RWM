namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rides", "ContactNumber", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Rides", "Contact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rides", "Contact", c => c.String());
            DropColumn("dbo.Rides", "ContactNumber");
        }
    }
}
