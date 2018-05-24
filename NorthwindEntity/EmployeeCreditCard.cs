using System;
using System.ComponentModel.DataAnnotations;

namespace NorthwindEntity
{
    public class EmployeeCreditCard
    {
        public int EmployeeCreditCardID { get; set; }

        [StringLength(30)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
