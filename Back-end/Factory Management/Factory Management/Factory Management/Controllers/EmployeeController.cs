using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Factory_Management.Models;

namespace Factory_Management.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        static EmployeeBL empBL = new EmployeeBL();
        // GET: api/    
        public IEnumerable<EmployeeFullData> Get()
        {
            return empBL.GetAllEmployees();
        }

        // GET: api/Employee/5
        public EmployeeFullData Get(int id)
        {
            return empBL.GetEmployee(id);
        }

        // GET: api/Employee/string
        public IEnumerable<EmployeeFullData> Get(string txt)
        {
            return empBL.GetEmployeesByName(txt);
        }


        // POST: api/Employee
        public string Post(Employee emp)
        {
            empBL.AddEmployee(emp);
            return "Created!";
        }

        // PUT: api/Employee/5
        public string Put(int id, Employee emp)
        {
            empBL.UpdateEmployee(id, emp);
            return "Updated!";
        }

        // DELETE: api/Employee/5
        public string Delete(int id)
        {
            empBL.DeleteEmployee(id);
            return "Deleted!";
        }
    }
}
