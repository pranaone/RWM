namespace Ridewithme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rides", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rides", "Contact");
        }
    }
}
