using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings
{
    class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ADP_DBContext context) : base(context) { /* */ }

        //Get all bookings, include full department record (not just FK reference)
        public IEnumerable<Booking> GetBookingsWithDepartments()
        {
            return allEntities
                .Include(b => b.Department)
                .OrderBy(b => b.Department)
                .ToList();
        }

        //Get all bookings made by a specific department
        public List<Booking> GetBookingsFromDepartment(int departmentID)
        {
            return allEntities
                .Where(b => b.Department.DepartmentID == departmentID)
                .ToList();
        }
        public List<Booking> GetBookingsFromDepartment(Department department)
        {
            return allEntities
                .Where(b => b.Department == department)
                .ToList();
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
