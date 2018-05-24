using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindEntity;

namespace DapperTest
{
    [TestClass]
    public class Task2Entity
    {
        private Northwind _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = new Northwind();
        }


        [TestMethod]
        public void Task3()
        {
            var result = from category in _context.Categories
                         where category.CategoryName == "Confections"
                         join product in _context.Products on category.CategoryID equals product.CategoryID
                         join details in _context.Order_Details on product.ProductID equals details.ProductID
                         join order in _context.Orders on details.OrderID equals order.OrderID
                         join customer in _context.Customers on order.CustomerID equals customer.CustomerID
                         select new
                         {
                             CategoryName = category.CategoryName,
                             CustomerName = customer.CompanyName,
                             Details = order.Order_Details,
                             Products = category.Products
                         };

            Assert.IsNotNull(result);
        }
    }
}

