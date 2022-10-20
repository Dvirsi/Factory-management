using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class EmployeeFullData
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StartWorkYear { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public List<string> Shifts { get; set; }
    }
}