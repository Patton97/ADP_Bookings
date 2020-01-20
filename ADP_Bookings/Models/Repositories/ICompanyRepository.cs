//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface ICompanyRepository : IRepository<Company>
    {
        //Get company, eagerload FKs

        //Get all companies, include full department record (not just FK reference)
        IEnumerable<Company> GetAll(bool includeDepartments);
    }
}
