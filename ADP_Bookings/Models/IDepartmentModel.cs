//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IDepartmentModel
    {
        // Retrieve all records in Departments table
        List<Department> GetAllDepartments();

        // Retrieve all records in Departments table belonging to a specific company
        List<Department> GetAllDepartmentsFrom(Company company);

        // Retrieve Department from specified ID
        Department FindDepartment(int companyID);
        Department FindDepartment(Department company);

        // Reports purely success/failure of Department retrieval
        bool DepartmentExists(Department company);

        // Save record to Departments table - determine whether to Create/Update
        void SaveDepartment(Department company);

        // Delete record in Departments table
        void DeleteDepartment(Department company);

        // The below functions access the CompanyRepo, which breaks SRP
        // but given time constraints this acts as a quick fix
        // Retrieve company from specified ID
        Company FindCompany(int companyID);
        Company FindCompany(Company company);
    }
}
