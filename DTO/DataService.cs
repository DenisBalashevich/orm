using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DTO.BusinessModel;
using DTO.Models;

namespace DTO
{
    public static class DataService
    {
        private static string connectionString =
          "Server=(local);Database=Northwind;Trusted_Connection=true";

        public static List<Category> GetAllCategory()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<Category>("select * from dbo.Categories").ToList();
            }
        }

        public static List<Product> GetAllProductWithCategoryAndSupplier()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                const string sql = "Select * from Products as p left join Categories as c on c.CategoryID = p.CategoryID left join Suppliers as s on s.SupplierID = p.SupplierID";

                return db.Query<Product, Category, Supplier, Product>(
                  sql,
                  (product, category, supplier) =>
                  {
                      product.Category = category;
                      product.Supplier = supplier;
                      return product;
                  },
                  splitOn: "CategoryID, SupplierID"
                ).ToList();
            }
        }

        public static List<EmployeeTerritory> GetAllEmployes()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                const string sql = "Select * from dbo.Employees as E left join dbo.EmployeeTerritories as ET on E.EmployeeID = ET.EmployeeID join dbo.Territories as T on ET.TerritoryID = T.TerritoryID";

                return db.Query<Employee, EmployeeTerritory, Territory, EmployeeTerritory>(
                  sql,
                  (emp, empTer, ter) =>
                  {
                      empTer.Employee = emp;
                      empTer.EmployeID = emp.EmployeeID;
                      empTer.Territory = ter;
                      empTer.TerritoryID = ter.TerritoryID;
                      return empTer;
                  },
                  splitOn: "EmployeeID,TerritoryID"
                ).ToList();
            }
        }

        public static IEnumerable<RegionStatistic> GetRegionStatistics()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                const string sql = "Select R.RegionID, R.RegionDescription, Count(R.RegionID) as 'EmployeesNumber' from dbo.Employees as E inner join dbo.EmployeeTerritories as ET on E.EmployeeID = ET.EmployeeID inner join dbo.Territories as T on ET.TerritoryID = T.TerritoryID inner join dbo.Regions as R on T.RegionID = R.RegionID group by R.RegionID,R.RegionDescription";

                return db.Query<RegionStatistic>(sql).ToList();
            }
        }

        public static IEnumerable<Tuple<int, int>> GetEmployeeShipers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                const string sql = "select distinct e.EmployeeID, s.ShipperID from Employees as e join Orders as o on e.EmployeeID = o.EmployeeID join Shippers as s on s.ShipperID = o.ShipVia	order by (e.EmployeeID)";

                return db.Query<Tuple<int, int>>(sql);
            }
        }

        public static void AddNewEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var id = db.Query<int>("insert Employees(FirstName, LastName) values (@FirstName, @LastName) select cast(scope_identity() as int)", employee).First();
                foreach (var er in employee.EmployeeTerritories)
                {
                    db.Execute("insert EmployeeTerritories(EmployeeID, TerritoryID) values (@empId, @terId)", new { empId = id, terId = er.TerritoryID });
                }
            }
        }

        public static void MoveProduct(int oldCategoryId, int newCategoryId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                db.Execute(
                  "update Products set CategoryID = (@oldCategoryId) where CategoryID = (@newCategoryId)",
                  new { oldCategoryId, newCategoryId});
            }
        }

        //Task 3.3

        public static IEnumerable<Category> GetCategoryesByName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                string sql = $"select Categories.CategoryID from Categories where Categories.CategoryName = '{name}'";

                return db.Query<Category>(sql);
            }
        }

        public static IEnumerable<Supplier> GetSuppliersByName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                string sql = $"select Suppliers.SupplierID from Suppliers where Suppliers.CompanyName = '{name}'";

                return db.Query<Supplier>(sql);
            }
        }

        public static int AddProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var categories = GetCategoryesByName(product.Category.CategoryName);
                int categoryId, supplierId;

                categoryId = categories?.FirstOrDefault()?.CategoryID ?? AddCategory(product.Category);

                var suppliers = GetSuppliersByName(product.Supplier.CompanyName);
                supplierId = suppliers?.FirstOrDefault()?.SupplierID ?? AddSupplier(product.Supplier);

                return db.Execute("insert Products(CategoryID, SupplierID, ProductName) values (@categoryID, @supplierID, @productName)", new { categoryID = categoryId, supplierID = supplierId, productName = product.ProductName });

            }
        }

        public static int AddCategory(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Execute("insert Categories(CategoryName, Description) values (@name, @description) select cast(scope_identity() as int)", new { name = category.CategoryName, description = category.Description });
            }
        }

        public static int AddSupplier(Supplier supplier)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Execute("insert Suppliers(CompanyName, Region) values (@name, @region) select cast(scope_identity() as int)", new { name = supplier.CompanyName, region = supplier.Region });
            }
        }
    }
}
