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

        //Get all bookings
        //bool param forces eager loading of FK data
        public IEnumerable<Booking> GetAll(bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Include(b => b.Department)
                    .Include(b => b.Activities)
                    .OrderBy(b => b.BookingID)
                    .ToList();
            else
                return GetAll();
        }

        //Get all bookings made by a specific department
        //bool param forces eager loading of FK data
        public List<Booking> GetAllBookingsFromDepartment(int departmentID, bool includeFKs = false)
        {
            return GetAll(includeFKs)
                .Where(b => b.Department.DepartmentID == departmentID)
                .ToList();
        }
        public List<Booking> GetAllBookingsFromDepartment(Department department, bool includeFKs = false)
        {
            return GetAllBookingsFromDepartment(department.DepartmentID, includeFKs);
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
