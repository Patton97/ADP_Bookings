using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public class BookingModel : RecordModel, IBookingModel
    {
        public BookingModel(IUnitOfWorkFactory unitOfWorkFactory = null) : base(unitOfWorkFactory) { }

        // Retrieve all records from Bookings table
        public List<Booking> GetAllBookings()
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Bookings.GetAll().ToList();
            }
        }

        // Retrieve all bookings belonging to a specified department
        public List<Booking> GetAllBookingsFrom(Department department)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Bookings.GetAllBookingsFromDepartment(department, true).ToList();
            }
        }

        // Retrieve booking from a specified ID
        public Booking FindBooking(int bookingID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Bookings.Get(bookingID);
            }
        }
        public Booking FindBooking(Booking booking) => FindBooking(booking.BookingID);

        // Reports purely on the success/failure of booking retrieval
        public bool BookingExists(Booking booking) => FindBooking(booking) != null;

        // Save record in Bookings table
        public void SaveBooking(Booking booking)
        {
            // Determine whether to Create/Update
            if (BookingExists(booking))
                UpdateBooking(booking);
            else
                CreateBooking(booking);
        }

        // Delete record from Bookings table
        public void DeleteBooking(Booking booking)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Bookings.Remove(unitOfWork.Bookings.Get(booking.BookingID));
                unitOfWork.SaveChanges();
            }
        }

        // The below functions are private to force any Presenter to simply 
        // call SaveDepartment(), and allow the Model to take over from there

        // Create new record in Bookings table
        private void CreateBooking(Booking booking)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Force booking to use correct department - ambiguity can arise leading to record duplication
                booking.Department = unitOfWork.Departments.Get(booking.Department.DepartmentID);
                unitOfWork.Bookings.Add(booking);
                unitOfWork.SaveChanges();
            }
        }

        // Update existing record in Bookings table
        private void UpdateBooking(Booking booking)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Force booking to use correct department - ambiguity can arise leading to record duplication
                booking.Department = unitOfWork.Departments.Get(booking.Department.DepartmentID);
                unitOfWork.Bookings.Update(booking);
                unitOfWork.SaveChanges();
            }
        }

        // The below functions access the DepartmentRepo, which breaks SRP
        // but given time constraints this acts as a quick fix

        // Retrieve Department from specified ID
        public Department FindDepartment(int departmentID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Departments.Get(departmentID, true);
            }
        }
        public Department FindDepartment(Department department) => FindDepartment(department.DepartmentID);
    }
}
