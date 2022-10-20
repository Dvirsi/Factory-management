using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class ShiftBL
    {
        Factory_Management_DBEntities1 db = new Factory_Management_DBEntities1();

        public List<ShiftFullData> GetAllShifts()
        {
            List<ShiftFullData> shifts = new List<ShiftFullData>();

            foreach (var shift in db.Shifts)
            {
                shifts.Add(GetShift(shift.ID));
            }
            return shifts;
        }


        public ShiftFullData GetShift(int id)
        {
            ShiftFullData shift = new ShiftFullData();

            var s = db.Shifts.Where(x => x.ID == id).First();

            shift.ID = s.ID;
            var correctDate = s.Date.ToString().Remove(10);
            shift.Date = correctDate;
            shift.StartTime = s.StartTime;
            shift.EndTime = s.EndTime;

            //Add list of employees 
            shift.Employees = new List<Employee>();

            //Add all employees id`s to list
            List<int> EmpsID = new List<int>();
            foreach (var IdS in db.Employee_Shift)
            {
                if (IdS.ShiftID == shift.ID)
                {
                    EmpsID.Add(IdS.EmployeeID);
                }
            }

            //Add the employees to current shift
            foreach (var e in db.Employees)
            {
                foreach (var empID in EmpsID)
                {
                    if (e.ID == empID)
                    {
                        shift.Employees.Add(e);
                    }
                }
            }
            return shift;
        }

        public void AddShift(Shift s)
        {
            Shift shif = new Shift();
   
            var dateString = s.Date;
            var dateObj = Convert.ToDateTime(dateString);
            shif.Date = dateObj;
            shif.StartTime = s.StartTime;
            shif.EndTime = s.EndTime;

            db.Shifts.Add(shif);
            db.SaveChanges();
        }

        public void AddShiftToEmp(Employee_Shift ShiftToEmp)
        {
            db.Employee_Shift.Add(ShiftToEmp);
            db.SaveChanges();
        }

        public void DeleteAllShiftForEmployee(int empID)
        {
            var empShift = db.Employee_Shift.Where(x => x.EmployeeID == empID).ToList();

            foreach(var shift in empShift)
            {
                db.Employee_Shift.Remove(shift);
            }

            db.SaveChanges();
        }
    }
}