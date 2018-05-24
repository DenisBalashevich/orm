namespace NorthwindEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNorthwindMigration_12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeCreditCards",
                c => new
                    {
                        EmployeeCreditCardID = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(maxLength: 30),
                        ExpirationDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeCreditCardID)
                .ForeignKey("Northwind.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeCreditCards", "EmployeeID", "Northwind.Employees");
            DropIndex("dbo.EmployeeCreditCards", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeCreditCards");
        }
    }
}
