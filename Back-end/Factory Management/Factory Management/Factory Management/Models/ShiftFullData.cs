using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class ShiftFullData
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<Employee> Employees { get; set; }

    }
}