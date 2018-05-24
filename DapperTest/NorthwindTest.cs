using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO;
using DTO.Models;

namespace DapperTest
{
    [TestClass]
    public class NorthwindTest
    {
        public string connectionString = "Server=(local);Database=Northwind;Trusted_Connection=true";
        [TestMethod]
        public void GetAllProductWithCategoryAndSupplier()
        {
            var actual = DataService.GetAllProductWithCategoryAndSupplier();

            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void GetRegionStatistics()
        {
            var actual = DataService.GetRegionStatistics();

            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void AddProduct()
        {
            var product = new Product
            {
                Category = new Category
                {
                    CategoryName = "Seafood22"
                },
                Supplier = new Supplier
                {
                    CompanyName = "OOOOO mops Compny223"
                },
                ProductName = "Shavuha"
            };
            var actual = DataService.AddProduct(product);

            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void AddSupplier()
        {
            var supplier = new Supplier
            {
                CompanyName = "OOO",
                Region = "sa"

            };
            var actual = DataService.AddSupplier(supplier);

            Assert.IsNotNull(actual);

        }
    }
}
