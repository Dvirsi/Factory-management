using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Factory_Management.Models;
using System.Web.Http.Cors;


    namespace Factory_Management.Controllers
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class DepartmentController : ApiController
    {
        static DepartmentBL deparBL = new DepartmentBL();
        // GET: api/Department
        public IEnumerable<DepartmentFullData> Get()
        {
            return deparBL.GetAllDepartment();
        }

        // GET: api/Department/5
        public DepartmentFullData Get(int id)
        {
            return deparBL.GetDepartment(id);
        }

        // GET: api/Department/5
        public DepartmentFullData Get(string depar)
        {
            return deparBL.GetDepartmentByName(depar);
        }


        // POST: api/Department
        public string Post(Department depar)
        {
            deparBL.AddDepartment(depar);
            return "Created!";
        }

        // PUT: api/Department/5
        public string Put(int id, Department depar)
        {
            deparBL.UpdateDepartment(id, depar);
            return "Updated!";
        }

        // DELETE: api/Department/5
        public string Delete(int id)
        {
            deparBL.DeleteDepartment(id);
            return "Deleted!";
        }
    }
}
