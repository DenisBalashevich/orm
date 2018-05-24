namespace NorthwindEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNorthwindMigration_13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Northwind.Shippers", "FoundationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Northwind.Shippers", "FoundationDate");
        }
    }
}
