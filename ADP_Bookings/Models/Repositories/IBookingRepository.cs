using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IBookingRepository : IRepository<Booking>
    {
        // Retrieve department record, eagerload FK data
         Booking Get(int id, bool includeFKs = false);

        //Get all bookings - bool param forces eager loading of FK data
        IEnumerable<Booking> GetAll(bool includeFKs = false);

        //Get all bookings made by a specific department
        List<Booking> GetAllBookingsFromDepartment(int departmentID, bool includeFKs = false);
        List<Booking> GetAllBookingsFromDepartment(Department department, bool includeFKs = false);

        //Force activities FK list to update
        void UpdateBookingActivities(Booking booking);
    }
}
