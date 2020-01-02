using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //Used as a store for all calls to the UoW from Presenter
    //static class because this solely acts as a window into the UoW
    static class DepartmentModel
    {
        public static List<Department> GetAllDepartments()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Departments.GetAll().ToList();
            }
        }
        public static List<Department> GetAllDepartmentsFrom(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Departments.GetDepartmentsFromCompany(company, true).ToList();
            }
        }

        public static List<Booking> GetAllBookingsFrom(Department department)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Bookings.GetAllBookingsFromDepartment(department, true).ToList();
            }
        }
        public static Department FindDepartment(Department department)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Departments.Get(department.DepartmentID);
            }
        }
        public static bool DepartmentExists(Department department) => FindDepartment(department) != null;

        public static void InsertNewDepartment(Department department)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                //Force department to use correct company - ambiguity can arise leading to record duplication
                department.Company = unitOfWork.Companies.Get(department.Company.CompanyID);
                unitOfWork.Departments.Add(department);
                unitOfWork.SaveChanges();
            }
        }

        public static void UpdateDepartment(Department department)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                //Force department to use correct company - ambiguity can arise leading to record duplication
                department.Company = unitOfWork.Companies.Get(department.Company.CompanyID);
                unitOfWork.Departments.Update(department);
                unitOfWork.SaveChanges();
            }
        }
    }
}
