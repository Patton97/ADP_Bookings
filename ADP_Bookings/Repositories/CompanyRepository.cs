using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings
{
    class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ADP_DBContext context) : base(context) { /* */ }

        //Get all bookings, include full department record (not just FK reference)
        public IEnumerable<Company> GetAll(bool includeDepartments = false)
        {

            if (includeDepartments)
                return allEntities
                    .Include(c => c.Departments)
                    .OrderBy(c => c.CompanyID);
            else
                return GetAll();
        }

        //This should/could force eager loading but doesn't atm for some reason 
        public IEnumerable<Company> GetAll2()
        {
            return allEntities
                    .Include(c => c.Departments)
                    .OrderBy(c => c.CompanyID);
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
