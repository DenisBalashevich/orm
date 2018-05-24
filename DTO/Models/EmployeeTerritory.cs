using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class EmployeeTerritory
    {
        public int EmployeID { get; set; }
        public Employee Employee { get; set; }
        public string TerritoryID { get; set; }
        public Territory Territory { get; set; }
    }
}
