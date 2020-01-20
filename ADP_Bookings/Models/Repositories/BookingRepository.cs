//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings.Models
{
    class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ADP_DBContext context) : base(context) { /* */ }

        // Retrieve department record, param toggles eager-loading FK data
        public Booking Get(int id, bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Where(b => b.BookingID == id)
                    .Include(b => b.Department)
                    .Include(b => b.Department.Company)
                    .Include(b => b.Activities)
                    .First();
            else
                return base.Get(id);
        }

        //Get all bookings, param toggles eager-loading FK data
        public IEnumerable<Booking> GetAll(bool includeFKs = false)
        {
            if (includeFKs)
                return allEntities
                    .Include(b => b.Department)
                    .Include(b => b.Activities)
                    .OrderBy(b => b.BookingID)
                    .ToList();
            else
                return base.GetAll();
        }

        //Get all bookings made by a specific department, param toggles eager-loading FK data
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

        public void UpdateBookingActivities(Booking booking)
        {
            Get(booking.BookingID).Activities = booking.Activities;
            allEntities.Attach(booking);
            Context.Entry(booking).State = EntityState.Modified;
        }

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
