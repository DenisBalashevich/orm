using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public string CustomerId { get; set; }

        public string EmployeeID { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public string ProductName { get; set; }

        public int ShipVia { get; set; }

        public string Freight { get; set; }

  
    }
}
