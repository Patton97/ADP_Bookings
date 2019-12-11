﻿using System;
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
        public IEnumerable<Company> GetCompaniesWithDepartments()
        {
            return allEntities
                .Include(c => c.Departments)
                .OrderBy(c => c.CompanyID)
                .ToList();
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
