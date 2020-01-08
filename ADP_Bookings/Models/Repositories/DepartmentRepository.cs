using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings.Models
{
    class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ADP_DBContext context) : base(context) { /* */ }

        // Retrieve department record, eagerload FK data
        public Department Get(int id, bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Where(d => d.DepartmentID == id)
                    .Include(d => d.Bookings)
                    .Include(d => d.Company)
                    .First();
            else
                return Get(id);
        }

        // Get all department records
        //bool param forces eager loading of FK data
        public IEnumerable<Department> GetAll(bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Include(d => d.Bookings)
                    .Include(d => d.Company)
                    .OrderBy(d => d.DepartmentID);
            else
                return GetAll();
        }

        //Get all departments belonging to specified company, include full record of company and any bookings (not just FK reference)
        public IEnumerable<Department> GetDepartmentsFromCompany(Company company, bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Where(d => d.Company.CompanyID == company.CompanyID)
                    .Include(d => d.Company)
                    .Include(d => d.Bookings)
                    .OrderBy(d => d.DepartmentID)
                    .ToList();
            else
                return allEntities
                    .Where(d => d.Company.CompanyID == company.CompanyID)
                    .OrderBy(d => d.DepartmentID)
                    .ToList();
        }
        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
