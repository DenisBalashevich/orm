using System.Collections.Generic;

namespace DTO.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
