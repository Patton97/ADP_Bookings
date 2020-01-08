using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        //Get all bookings - bool param forces eager loading of FK data
        IEnumerable<Department> GetAll(bool includeFKs = false);
        IEnumerable<Department> GetDepartmentsFromCompany(Company company, bool includeFKs = false);
    }
}
