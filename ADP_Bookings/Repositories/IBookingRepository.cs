using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings
{
    public interface IBookingRepository : IRepository<Booking>
    {
        //Get all bookings, include full department record (not just FK reference)
        IEnumerable<Booking> GetBookingsWithDepartments();

        //Get all bookings made by a specific department
        List<Booking> GetBookingsFromDepartment(int departmentID);
        List<Booking> GetBookingsFromDepartment(Department department);
    }
}
