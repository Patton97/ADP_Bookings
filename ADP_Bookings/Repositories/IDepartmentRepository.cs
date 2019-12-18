using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        //Get all bookings, include full department record (not just FK reference)
        IEnumerable<Department> GetDepartmentsWithBookings();
        IEnumerable<Department> GetDepartmentsWithCompany();
        IEnumerable<Department> GetDepartmentsFromCompany(Company company);
    }
}
