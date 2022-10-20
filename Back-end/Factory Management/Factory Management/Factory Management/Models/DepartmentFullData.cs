using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class DepartmentFullData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManagerID { get; set; }
        public string Manager { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
