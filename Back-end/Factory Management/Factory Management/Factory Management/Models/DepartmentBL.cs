using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class DepartmentBL
    {
        Factory_Management_DBEntities1 db = new Factory_Management_DBEntities1();

        public DepartmentFullData GetDepartment(int id)
        {
            var currentDepar = db.Departments.Where(x => x.ID == id).First();

            var depar = new DepartmentFullData();
            depar.ID = currentDepar.ID;
            depar.Name = currentDepar.Name;

            //Add list of employees
            depar.Employees = new List<Employee>();
            var emps = db.Employees.Where(x => x.DepartmentID == depar.ID).ToList();
            depar.Employees = emps;

            List<int> empsDeparId = new List<int>();
            foreach (Employee emp in depar.Employees)
            {
                empsDeparId.Add(emp.DepartmentID);
            }

            try
            {
                depar.ManagerID = (int)currentDepar.ManagerID;
                var managerDepar = db.Employees.Where(x => x.ID == depar.ManagerID).First();
                depar.Manager = managerDepar.FirstName + " " + managerDepar.LastName;
            }
            catch
            {
                depar.ManagerID = 0;
                depar.Manager = "";
            }

            return depar;
        }

        public List<DepartmentFullData> GetAllDepartment()
        {
            List<DepartmentFullData> departments = new List<DepartmentFullData>();

            foreach (var depar in db.Departments)
            {
                departments.Add(GetDepartment(depar.ID));
            }
            return departments;
        }

        public DepartmentFullData GetDepartmentByName(string depar)
        {
            var departments = GetAllDepartment();

            try
            {
                return departments.Where(x => x.Name == depar).First();
            }
            catch
            {
                return null;
            }
        }

        public void AddDepartment(Department dep)
        {
            db.Departments.Add(dep);
            db.SaveChanges();
        }

        public void UpdateDepartment(int id, Department d)
        {
            var currentDepar = db.Departments.Where(x => x.ID == id).First();

            currentDepar.Name = d.Name;
            currentDepar.ManagerID = d.ManagerID;

            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var deparToDelete = db.Departments.Where(x => x.ID == id).First();

            db.Departments.Remove(deparToDelete);
            db.SaveChanges();
        }
    }
}
