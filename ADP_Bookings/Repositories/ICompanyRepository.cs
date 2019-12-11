using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings
{
    public interface ICompanyRepository : IRepository<Company>
    {
        //Get all companies, include full department record (not just FK reference)
        IEnumerable<Company> GetCompaniesWithDepartments();
    }
}
