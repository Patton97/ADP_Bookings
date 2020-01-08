﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings.Models
{
    class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ADP_DBContext context) : base(context) { /* */ }

        //Get all companies
        //bool param forces eager loading of FK data
        public IEnumerable<Company> GetAll(bool includeDepartments = false)
        {

            if (includeDepartments)
                return allEntities
                    .Include(c => c.Departments)
                    .OrderBy(c => c.CompanyID);
            else
                return GetAll();
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}