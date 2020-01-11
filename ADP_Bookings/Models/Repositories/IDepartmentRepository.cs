using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        // Retrieve department record, eagerload FK data
        Department Get(int id, bool includeFKs);

        //Get all bookings - bool param forces eager loading of FK data
        IEnumerable<Department> GetAll(bool includeFKs);
        IEnumerable<Department> GetDepartmentsFromCompany(Company company, bool includeFKs);
    }
}
