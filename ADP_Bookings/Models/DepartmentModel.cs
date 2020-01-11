using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public static class DepartmentModel
    {
        // Create new record in Departments table
        public static void InsertNewDepartment(Department department, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                //Force department to use correct company - ambiguity can arise leading to record duplication
                department.Company = unitOfWork.Companies.Get(department.Company.CompanyID);
                unitOfWork.Departments.Add(department);
                unitOfWork.SaveChanges();
            }
        }

        // Retrieve all records in Departments table
        public static List<Department> GetAllDepartments(IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                return unitOfWork.Departments.GetAll().ToList();
            }
        }

        // Retrieve all departments belonging to a specfied company
        public static List<Department> GetAllDepartmentsFrom(Company company, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                return unitOfWork.Departments.GetDepartmentsFromCompany(company, true).ToList();
            }
        }

        // Retrieve Department from specified ID
        public static Department FindDepartment(int departmentID, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                return unitOfWork.Departments.Get(departmentID);
            }
        }
        public static Department FindDepartment(Department department, IUnitOfWork uow) => FindDepartment(department.DepartmentID, uow);

        // Reports purely success/failure of company retrieval
        public static bool DepartmentExists(Department department, IUnitOfWork uow) => FindDepartment(department, uow) != null;

        // Update existing record in Departments table
        public static void UpdateDepartment(Department department, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                //Force department to use correct company - ambiguity can arise leading to record duplication
                department.Company = unitOfWork.Companies.Get(department.Company.CompanyID);
                unitOfWork.Departments.Update(department);
                unitOfWork.SaveChanges();
            }
        }

        // Delete record from Departments table
        public static void DeleteDepartment(Department department, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                //Ensure record actually exists before attempting to delete
                //Current uow var is sent so both operations occur within same UoW
                if (!DepartmentExists(department, uow))
                {
                    Console.WriteLine("ERROR: Record delete failed!\n"
                                    + "       Department: #" + department.DepartmentID + "could not be found.");
                }
                else
                {
                    unitOfWork.Departments.Remove(unitOfWork.Departments.Get(department.DepartmentID));
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
