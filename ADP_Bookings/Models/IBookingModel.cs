//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IBookingModel
    {
        // Retrieve all records from Bookings table
        List<Booking> GetAllBookings();

        // Retrieve all bookings belonging to a specified department
        List<Booking> GetAllBookingsFrom(Department department);

        // Retrieve booking from a specified ID
        Booking FindBooking(int bookingID);
        Booking FindBooking(Booking booking);

        // Reports purely on the success/failure of booking retrieval
        bool BookingExists(Booking booking);

        // Save record in Bookings table
        void SaveBooking(Booking booking);

        // Delete record from Bookings table
        void DeleteBooking(Booking booking);

        // The below functions access the DepartmentRepo, which breaks SRP
        // but given time constraints this acts as a quick fix

        // Retrieve Department from specified ID
        Department FindDepartment(int departmentID);
        Department FindDepartment(Department department);
    }
}
