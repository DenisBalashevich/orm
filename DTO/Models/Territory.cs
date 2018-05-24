using System.Collections.Generic;

namespace DTO.Models
{
    public class Territory
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }

        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
