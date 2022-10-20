using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class EmployeeBL
    {
        Factory_Management_DBEntities1 db = new Factory_Management_DBEntities1();

        public List<EmployeeFullData> GetEmployeesByName(string txt)
        {
            //Change first letter to uppercase if enterted lowercase
            if (txt != null)
            {
                if (Char.IsLower(txt[0]))
                {
                    txt = char.ToUpper(txt[0]) + txt.Substring(1).ToLower();
                }
            }

            // Return all employees if the user click search without value
            var employees = GetAllEmployees();
            if (txt == null)
            {
                return GetAllEmployees();
            }

            // Add all result from first name
            var empsFirstName = employees.Where(x => x.FirstName.StartsWith(txt)).ToList();
            var empsLastName = employees.Where(x => x.LastName.StartsWith(txt)).ToList();
            var empsDepartmentName = employees.Where(x => x.Department.StartsWith(txt)).ToList();
            var result = empsFirstName;

            // Add all result from last name
            foreach (var last in empsLastName)
            {
                if (!(result.Contains(last)))
                {
                    result.Add(last);
                }
            }

            // Add all result from department name
            foreach (var depar in empsDepartmentName)
            {
                if (!(result.Contains(depar)))
                {
                    result.Add(depar);
                }
            }
            return result;
        }

        public List<EmployeeFullData> GetAllEmployees()
        {
            List<EmployeeFullData> employees = new List<EmployeeFullData>();

            foreach (var emp in db.Employees)
            {
                employees.Add(GetEmployee(emp.ID));
            }
            return employees;
        }

        public EmployeeFullData GetEmployee(int id)
        {
            var currentEmployee = db.Employees.Where(x => x.ID == id).First();

            EmployeeFullData emp = new EmployeeFullData();

            emp.ID = currentEmployee.ID;
            emp.FirstName = currentEmployee.FirstName;
            emp.LastName = currentEmployee.LastName;
            emp.StartWorkYear = currentEmployee.StartWorkYear;
            emp.DepartmentID = currentEmployee.DepartmentID;

            //Check if this this employee belong to department
            if (db.Departments.Where(x => x.ID == emp.DepartmentID).ToList().Count > 0)
            {
                var depar = db.Departments.Where(x => x.ID == emp.DepartmentID).First();
                emp.Department = depar.Name;
            }
            else
            {
                emp.Department = "";
            }

            //Add list of shift for all Employees
            emp.Shifts = new List<string>();

            var EmpShiftsIDs = db.Employee_Shift.Where(x => x.EmployeeID == emp.ID).ToList();

            //Get list with all shifts data
            foreach (var data in EmpShiftsIDs)
            {
                foreach (var dates in db.Shifts)
                {
                    if (data.ShiftID == dates.ID)
                    {
                        var correctDate = dates.Date.ToString();
                        var datesAndTime =
                            "Date: "
                            + correctDate.Remove(10)
                            + ", Start time:"
                            + dates.StartTime
                            + ", End time: "
                            + dates.EndTime;
                        emp.Shifts.Add(datesAndTime);
                    }
                }
            }
            return emp;
        }

        public void AddEmployee(Employee emp)
        {
            db.Employees.Add(emp);

            db.SaveChanges();
        }

        public void UpdateEmployee(int id, Employee e)
        {
            Employee emp = db.Employees.Where(x => x.ID == id).First();

            emp.FirstName = e.FirstName;
            emp.LastName = e.LastName;
            emp.StartWorkYear = e.StartWorkYear;

            Department depar = db.Departments.Where(x => x.ID == emp.DepartmentID).First();
            if (depar.ManagerID == emp.ID && depar.ID != e.DepartmentID)
            {
                depar.ManagerID = 0;
            }

            emp.DepartmentID = e.DepartmentID;

            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var emp = db.Employees.Where(x => x.ID == id).First();

            //DeleteEmployee all employee shifts
            ShiftBL shifts = new ShiftBL();
            shifts.DeleteAllShiftForEmployee(id);

            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}
