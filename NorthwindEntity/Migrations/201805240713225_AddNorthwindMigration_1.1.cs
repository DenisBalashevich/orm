namespace NorthwindEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNorthwindMigration_11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Northwind.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        Description = c.String(storeType: "ntext"),
                        Picture = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "Northwind.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        SupplierID = c.Int(),
                        CategoryID = c.Int(),
                        QuantityPerUnit = c.String(maxLength: 20),
                        UnitPrice = c.Decimal(storeType: "money"),
                        UnitsInStock = c.Short(),
                        UnitsOnOrder = c.Short(),
                        ReorderLevel = c.Short(),
                        Discontinued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("Northwind.Categories", t => t.CategoryID)
                .ForeignKey("Northwind.Suppliers", t => t.SupplierID)
                .Index(t => t.SupplierID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "Northwind.Order Details",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("Northwind.Orders", t => t.OrderID)
                .ForeignKey("Northwind.Products", t => t.ProductID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "Northwind.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(maxLength: 5, fixedLength: true),
                        EmployeeID = c.Int(),
                        OrderDate = c.DateTime(),
                        RequiredDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        ShipVia = c.Int(),
                        Freight = c.Decimal(storeType: "money"),
                        ShipName = c.String(maxLength: 40),
                        ShipAddress = c.String(maxLength: 60),
                        ShipCity = c.String(maxLength: 15),
                        ShipRegion = c.String(maxLength: 15),
                        ShipRegiongtre = c.String(maxLength: 15),
                        ShipPostalCode = c.String(maxLength: 10),
                        ShipCountry = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("Northwind.Customers", t => t.CustomerID)
                .ForeignKey("Northwind.Employees", t => t.EmployeeID)
                .ForeignKey("Northwind.Shippers", t => t.ShipVia)
                .Index(t => t.CustomerID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ShipVia);
            
            CreateTable(
                "Northwind.Customers",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 5, fixedLength: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        ContactName = c.String(maxLength: 30),
                        ContactTitle = c.String(maxLength: 30),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "Northwind.CustomerDemographics",
                c => new
                    {
                        CustomerTypeID = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        CustomerDesc = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.CustomerTypeID);
            
            CreateTable(
                "Northwind.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        Title = c.String(maxLength: 30),
                        TitleOfCourtesy = c.String(maxLength: 25),
                        BirthDate = c.DateTime(),
                        HireDate = c.DateTime(),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        HomePhone = c.String(maxLength: 24),
                        Extension = c.String(maxLength: 4),
                        Photo = c.Binary(storeType: "image"),
                        Notes = c.String(storeType: "ntext"),
                        ReportsTo = c.Int(),
                        PhotoPath = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("Northwind.Employees", t => t.ReportsTo)
                .Index(t => t.ReportsTo);
            
            CreateTable(
                "Northwind.Territories",
                c => new
                    {
                        TerritoryID = c.String(nullable: false, maxLength: 20),
                        TerritoryDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TerritoryID)
                .ForeignKey("Northwind.Region", t => t.RegionID)
                .Index(t => t.RegionID);
            
            CreateTable(
                "Northwind.Region",
                c => new
                    {
                        RegionID = c.Int(nullable: false),
                        RegionDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "Northwind.Shippers",
                c => new
                    {
                        ShipperID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.ShipperID);
            
            CreateTable(
                "Northwind.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        ContactName = c.String(maxLength: 30),
                        ContactTitle = c.String(maxLength: 30),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                        HomePage = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "Northwind.CustomerCustomerDemo",
                c => new
                    {
                        CustomerTypeID = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        CustomerID = c.String(nullable: false, maxLength: 5, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.CustomerTypeID, t.CustomerID })
                .ForeignKey("Northwind.CustomerDemographics", t => t.CustomerTypeID, cascadeDelete: true)
                .ForeignKey("Northwind.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerTypeID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "Northwind.EmployeeTerritories",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        TerritoryID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.TerritoryID })
                .ForeignKey("Northwind.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("Northwind.Territories", t => t.TerritoryID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TerritoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Northwind.Products", "SupplierID", "Northwind.Suppliers");
            DropForeignKey("Northwind.Order Details", "ProductID", "Northwind.Products");
            DropForeignKey("Northwind.Orders", "ShipVia", "Northwind.Shippers");
            DropForeignKey("Northwind.Order Details", "OrderID", "Northwind.Orders");
            DropForeignKey("Northwind.EmployeeTerritories", "TerritoryID", "Northwind.Territories");
            DropForeignKey("Northwind.EmployeeTerritories", "EmployeeID", "Northwind.Employees");
            DropForeignKey("Northwind.Territories", "RegionID", "Northwind.Region");
            DropForeignKey("Northwind.Orders", "EmployeeID", "Northwind.Employees");
            DropForeignKey("Northwind.Employees", "ReportsTo", "Northwind.Employees");
            DropForeignKey("Northwind.Orders", "CustomerID", "Northwind.Customers");
            DropForeignKey("Northwind.CustomerCustomerDemo", "CustomerID", "Northwind.Customers");
            DropForeignKey("Northwind.CustomerCustomerDemo", "CustomerTypeID", "Northwind.CustomerDemographics");
            DropForeignKey("Northwind.Products", "CategoryID", "Northwind.Categories");
            DropIndex("Northwind.EmployeeTerritories", new[] { "TerritoryID" });
            DropIndex("Northwind.EmployeeTerritories", new[] { "EmployeeID" });
            DropIndex("Northwind.CustomerCustomerDemo", new[] { "CustomerID" });
            DropIndex("Northwind.CustomerCustomerDemo", new[] { "CustomerTypeID" });
            DropIndex("Northwind.Territories", new[] { "RegionID" });
            DropIndex("Northwind.Employees", new[] { "ReportsTo" });
            DropIndex("Northwind.Orders", new[] { "ShipVia" });
            DropIndex("Northwind.Orders", new[] { "EmployeeID" });
            DropIndex("Northwind.Orders", new[] { "CustomerID" });
            DropIndex("Northwind.Order Details", new[] { "ProductID" });
            DropIndex("Northwind.Order Details", new[] { "OrderID" });
            DropIndex("Northwind.Products", new[] { "CategoryID" });
            DropIndex("Northwind.Products", new[] { "SupplierID" });
            DropTable("Northwind.EmployeeTerritories");
            DropTable("Northwind.CustomerCustomerDemo");
            DropTable("Northwind.Suppliers");
            DropTable("Northwind.Shippers");
            DropTable("Northwind.Region");
            DropTable("Northwind.Territories");
            DropTable("Northwind.Employees");
            DropTable("Northwind.CustomerDemographics");
            DropTable("Northwind.Customers");
            DropTable("Northwind.Orders");
            DropTable("Northwind.Order Details");
            DropTable("Northwind.Products");
            DropTable("Northwind.Categories");
        }
    }
}
