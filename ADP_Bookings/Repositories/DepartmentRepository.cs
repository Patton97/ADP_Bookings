using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings
{
    class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ADP_DBContext context) : base(context) { /* */ }

        //Get all departments, include full booking record for all bookings made by department
        //(not just FK reference)
        public IEnumerable<Department> GetDepartmentsWithBookings()
        {
            return allEntities
                .Include(d => d.Bookings)
                .OrderBy(d => d.DepartmentID)
                .ToList();
        }
        //Get all departments, include full company record (not just FK reference)
        public IEnumerable<Department> GetDepartmentsWithCompany()
        {
            return allEntities
                .Include(d => d.Company)
                .OrderBy(d => d.DepartmentID)
                .ToList();
        }

        public IEnumerable<Department> GetDepartmentsFromCompany(Company company)
        {
            return allEntities
                .Where(d => d.Company.CompanyID == company.CompanyID)
                .Include(d => d.Company)
                .OrderBy(d => d.DepartmentID)
                .ToList();
        }


        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
