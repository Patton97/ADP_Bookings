using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public class DepartmentModel : IDepartmentModel
    {
        public DepartmentModel() { }

        private IUnitOfWork GetNewUnitOfWork() => new UnitOfWork(new ADP_DBContext());

        // Retrieve all records in Departments table
        public List<Department> GetAllDepartments()
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Departments.GetAll(true).ToList(); //bool param specifies eager loading of FK data
            }
        }

        // Retrieve all records in Departments table
        public List<Department> GetAllDepartmentsFrom(Company company)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Departments.GetDepartmentsFromCompany(company,true).ToList(); //bool param specifies eager loading of FK data
            }
        }

        // Retrieve department from specified ID
        public Department FindDepartment(int departmentID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Departments.Get(departmentID);
            }
        }
        public Department FindDepartment(Department department) => FindDepartment(department.DepartmentID);

        // Reports purely success/failure of department retrieval
        public bool DepartmentExists(Department department) => FindDepartment(department) != null;

        // Save record in Departments table
        public void SaveDepartment(Department department)
        {
            // Determine whether to Create/Update
            if (DepartmentExists(department))
                UpdateDepartment(department);
            else
                CreateDepartment(department);
        }

        // Delete record in Departments table
        public void DeleteDepartment(Department department)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Ensure record actually exists before attempting to delete
                if (!DepartmentExists(department))
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

        // The below functions are private to force any Presenter to simply 
        // call SaveDepartment(), and allow the Model to take over from there

        // Create new record in Departments table
        private void CreateDepartment(Department department)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Departments.Add(department);
                unitOfWork.SaveChanges();
            }
        }

        // Update existing record in Departments table
        private void UpdateDepartment(Department department)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Departments.Update(department);
                unitOfWork.SaveChanges();
            }
        }

        // The below functions access the CompanyRepo, which breaks SRP
        // but given time constraints this acts as a quick fix

        // Retrieve company from specified ID
        public Company FindCompany(int companyID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Companies.Get(companyID);
            }
        }
        public Company FindCompany(Company company) => FindCompany(company.CompanyID);
    }
}
