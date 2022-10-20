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
    public class ShiftController : ApiController
    {
        static ShiftBL shiftBL = new ShiftBL();
        // GET: api/Shift
        public IEnumerable<ShiftFullData> Get()
        {
            return shiftBL.GetAllShifts();
        }

        // GET: api/Shift/5
        public ShiftFullData Get(int id)
        {
            return shiftBL.GetShift(id);
        }

        // POST: api/Shift
        public string Post(Shift shif)
        {
            shiftBL.AddShift(shif);
            return "Created!";
        }


        //Cant use 2 Post in the same controller with 1 parameter so i gave id and didnt use him
        // POST: api/Shift
        public string Post(int id, Employee_Shift newShiftToEmp)
        {
            shiftBL.AddShiftToEmp(newShiftToEmp);
            return "Created!";
        }

        // PUT: api/Shift/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Shift/5
        public void Delete(int id)
        {
        }
    }
}
