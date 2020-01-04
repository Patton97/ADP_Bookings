using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //Used as a store for all calls to the UoW from Presenter
    //static class because this solely acts as a window into the UoW
    static class BookingModel
    {
        public static List<Booking> GetAllBookings()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Bookings.GetAll().ToList();
            }
        }

        public static List<Booking> GetAllBookingsFrom(Department department)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Bookings.GetAllBookingsFromDepartment(department, true).ToList();
            }
        }

        public static Booking FindBooking(Booking booking)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Bookings.Get(booking.BookingID);
            }
        }
        public static bool BookingExists(Booking booking) => FindBooking(booking) != null;

        public static void InsertNewBooking(Booking booking)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                //Force booking to use correct department - ambiguity can arise leading to record duplication
                booking.Department = unitOfWork.Departments.Get(booking.Department.DepartmentID);
                unitOfWork.Bookings.Add(booking);
                unitOfWork.SaveChanges();
            }
        }

        public static void UpdateBooking(Booking booking)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                //Force booking to use correct department - ambiguity can arise leading to record duplication
                booking.Department = unitOfWork.Departments.Get(booking.Department.DepartmentID);
                unitOfWork.Bookings.Update(booking);
                unitOfWork.SaveChanges();
            }
        }

        public static void DeleteBooking(Booking booking)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Bookings.Remove(unitOfWork.Bookings.Get(booking.BookingID));
                unitOfWork.SaveChanges();
            }
        }
    }
}
